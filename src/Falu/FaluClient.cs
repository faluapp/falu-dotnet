﻿using Falu.Identity;
using Falu.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Falu
{
    /// <summary>
    /// Official client for Falu API
    /// </summary>
    public sealed partial class FaluClient
    {
        private readonly string JsonContentType = System.Net.Mime.MediaTypeNames.Application.Json;

        private readonly HttpClient backChannel;
        private readonly FaluClientOptions options;

        /// <summary>
        /// Creates an instance of <see cref="FaluClient"/>
        /// </summary>
        /// <param name="backChannel"></param>
        /// <param name="optionsAccessor"></param>
        public FaluClient(HttpClient backChannel, IOptions<FaluClientOptions> optionsAccessor)
        {
            this.backChannel = backChannel ?? throw new ArgumentNullException(nameof(backChannel));
            options = optionsAccessor.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        private Uri BaseAddress => backChannel.BaseAddress;

        /// <summary>
        /// Search for an entity's identity.
        /// </summary>
        /// <param name="search">The details to use for searching.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<IdentityRecord>> SearchIdentityAsync(IdentitySearchModel search,
                                                                                CancellationToken cancellationToken = default)
        {
            if (search is null) throw new ArgumentNullException(nameof(search));

            var uri = new Uri(BaseAddress, "/v1/identity/search");
            return await PostAsJsonAsync<IdentityRecord>(uri, search, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Fetch restricted identity data for marketing purposes.
        /// Sensitive data is excluded in the response. The corresponsing properties will be null.
        /// </summary>
        /// <param name="model">Starting date for the payments</param>
        /// <param name="count">Maximum number of items to return</param>
        /// <param name="continuationToken">The continuation token from a previous request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<List<IdentityRecord>>> MarketingIdentityAsync(IdentityMarketingQuery model = null,
                                                                                         int? count = null,
                                                                                         string continuationToken = null,
                                                                                         CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            if (count != null) args["count"] = $"{count}";
            if (!string.IsNullOrWhiteSpace(continuationToken)) args["ct"] = continuationToken;

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/identity/marketing{query}");
            return await PostAsJsonAsync<List<IdentityRecord>>(uri, model, cancellationToken: cancellationToken);
        }

        #region Helpers

        private async Task<ResourceResponse<TResource>> GetAsJsonAsync<TResource>(Uri uri, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(JsonContentType));
            return await SendAsync<TResource>(request, cancellationToken);
        }

        private async Task<ResourceResponse<TResource>> PatchAsJsonAsync<TResource>(Uri uri,
                                                                                    object patch,
                                                                                    Encoding encoding = null,
                                                                                    CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
            {
                Content = await MakeJsonHttpContentAsync(patch, encoding, cancellationToken)
            };
            return await SendAsync<TResource>(request, cancellationToken);
        }

        private async Task<ResourceResponse<TResource>> PostAsJsonAsync<TResource>(Uri uri,
                                                                                   object o,
                                                                                   Encoding encoding = null,
                                                                                   CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = await MakeJsonHttpContentAsync(o, encoding, cancellationToken),
            };
            return await SendAsync<TResource>(request, cancellationToken);
        }

        private async Task<ResourceResponse<TResource>> SendAsync<TResource>(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            var response = await SendAsync(request, cancellationToken);
            var resource = default(TResource);
            var error = default(FaluError);

            // get the content type
            var contentType = response.Content.Headers?.ContentType;

            //  TODO: check if content type matches expectation

            // get a stream reference for the content
            // the stream may still be being incoming and thus we should only read when necessary
            var stream = await response.Content.ReadAsStreamAsync();

            // if the response was a success then deserialize the body as TResource otherwise TError
            if (response.IsSuccessStatusCode)
            {
                resource = await DeserializeAsync<TResource>(stream, cancellationToken);
            }
            else
            {
                error = await DeserializeAsync<FaluError>(stream, cancellationToken);
            }
            return new ResourceResponse<TResource>(response: response, resource: resource, error: error);
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            // ensure request is not null
            if (request == null) throw new ArgumentNullException(nameof(request));

            request.Headers.Add("X-Falu-Version", FaluClientOptions.ApiVersion);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", options.ApiKey);

            // execute the request
            return await backChannel.SendAsync(request, cancellationToken);
        }

        private async Task<HttpContent> MakeJsonHttpContentAsync(object o, Encoding encoding, CancellationToken cancellationToken)
        {
            encoding ??= Encoding.UTF8;
            var stream = await SerializeAsync(o, cancellationToken);
            var content = new StreamContent(stream);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse($"{JsonContentType};charset={encoding.BodyName}");
            return content;
        }

        private async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken)
        {
            using (stream)
            {
                if (typeof(Stream).IsAssignableFrom(typeof(T))) return (T)(object)stream;

                // if the stream is empty return the default
                if (stream.Length == 0) return default;

                return await JsonSerializer.DeserializeAsync<T>(utf8Json: stream,
                                                                options: options.SerializerOptions,
                                                                cancellationToken: cancellationToken);
            }
        }

        private async Task<Stream> SerializeAsync<T>(T input, CancellationToken cancellationToken)
        {
            var payload = new MemoryStream();
            await JsonSerializer.SerializeAsync<T>(utf8Json: payload,
                                                   value: input,
                                                   options: options.SerializerOptions,
                                                   cancellationToken: cancellationToken);

            // make the produced payload readable
            payload.Position = 0;
            return payload;
        }

        #endregion
    }
}