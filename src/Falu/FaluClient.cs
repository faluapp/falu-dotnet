using Falu.Identity;
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
        /// Find a persons identity either by national identification number or phone number.
        /// </summary>
        /// <param name="nationalId">the unique identifier of the person</param>
        /// <param name="phoneNumber">the phone number to query</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<IdentitySearchResult>> SearchIdentityAsync(string nationalId = null,
                                                                                      string phoneNumber = null,
                                                                                      CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(nationalId) && string.IsNullOrWhiteSpace(phoneNumber))
                throw new InvalidOperationException($"Either '{nameof(nationalId)}' or '{nameof(phoneNumber)}' must be provided");

            var args = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(nationalId)) args["idNumber"] = nationalId;
            if (!string.IsNullOrWhiteSpace(phoneNumber)) args["phoneNumber"] = phoneNumber;

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/identity/search{query}");
            return await GetAsJsonAsync<IdentitySearchResult>(uri, cancellationToken: cancellationToken);
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
