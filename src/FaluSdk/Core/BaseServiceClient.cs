using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Core
{
    ///
    public abstract class BaseServiceClient // This class exists because not all service clients may be based on a resource
    {
        /// <summary>List of supported JSON content types</summary>
        public static readonly string[] SupportedContentTypes = new[] {
            "application/json",
            "text/json",
            "application/json-path+json",
            "application/*+json",
            "application/problem+json",
        };

        private readonly string JsonContentType = SupportedContentTypes[0];

        ///
        protected BaseServiceClient(HttpClient backChannel, FaluClientOptions options)
        {
            BackChannel = backChannel ?? throw new ArgumentNullException(nameof(backChannel));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        ///
        protected HttpClient BackChannel { get; }

        ///
        protected FaluClientOptions Options { get; }

        #region Helpers

        ///
        protected virtual async Task<ResourceResponse<TResource>> RequestAsync<TResource>(string uri,
                                                                                          HttpMethod method,
                                                                                          object o,
                                                                                          RequestOptions? options = null,
                                                                                          CancellationToken cancellationToken = default)
        {
            var content = await MakeJsonHttpContentAsync(o, cancellationToken).ConfigureAwait(false);
            return await RequestAsync<TResource>(uri, method, content, options, cancellationToken).ConfigureAwait(false);
        }

        ///
        protected virtual async Task<ResourceResponse<TResource>> RequestAsync<TResource>(string uri,
                                                                                          HttpMethod method,
                                                                                          HttpContent? content = null,
                                                                                          RequestOptions? options = null,
                                                                                          CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, uri);
            if (content is not null)
            {
                request.Content = content;
            }

            return await RequestCoreAsync<TResource>(request, options, cancellationToken).ConfigureAwait(false);
        }

        ///
        protected virtual async Task<ResourceResponse<TResource>> RequestCoreAsync<TResource>(HttpRequestMessage request,
                                                                                              RequestOptions? options = null,
                                                                                              CancellationToken cancellationToken = default)
        {
            var response = await RequestCoreAsync(request, options, cancellationToken).ConfigureAwait(false);
            var resource = default(TResource);
            var error = default(FaluError);

            // get the content type
            var contentType = response.Content.Headers?.ContentType;

            // get a stream reference for the content
            // the stream may still be being incoming and thus we should only read when necessary
#if NET5_0_OR_GREATER
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#else
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#endif

            // if the response was a success then deserialize the body as TResource otherwise TError
            if (response.IsSuccessStatusCode)
            {
                resource = await DeserializeAsync<TResource>(contentType?.MediaType, stream, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                error = await DeserializeAsync<FaluError>(contentType?.MediaType, stream, cancellationToken).ConfigureAwait(false);
            }
            return new ResourceResponse<TResource>(response: response, resource: resource, error: error);
        }

        ///
        protected virtual async Task<HttpResponseMessage> RequestCoreAsync(HttpRequestMessage request,
                                                                           RequestOptions? options = null,
                                                                           CancellationToken cancellationToken = default)
        {
            // ensure request is not null
            if (request == null) throw new ArgumentNullException(nameof(request));

            request.Headers.Add(HeadersNames.XFaluVersion, FaluClientOptions.ApiVersion);
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(JsonContentType));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Options.ApiKey);

            options ??= new RequestOptions(); // allows for code below to run

            if (!string.IsNullOrWhiteSpace(options.IdempotencyKey))
            {
                request.Headers.Add(HeadersNames.XIdempotencyKey, options.IdempotencyKey);
            }
            else if (request.Method == HttpMethod.Patch || request.Method == HttpMethod.Post) // add IdempotencyKey to allow to automatic retries
            {
                request.Headers.Add(HeadersNames.XIdempotencyKey, Guid.NewGuid().ToString());
            }

            // only for user bearer token
            if (!string.IsNullOrWhiteSpace(options.Workspace))
            {
                request.Headers.Add(HeadersNames.XWorkspaceId, options.Workspace);
            }

            // only for user bearer token
            if (options.Live is not null)
            {
                request.Headers.Add(HeadersNames.XLiveMode, options.Live.Value.ToString().ToLowerInvariant());
            }

            // execute the request
            return await BackChannel.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private async Task<HttpContent> MakeJsonHttpContentAsync(object o, CancellationToken cancellationToken)
        {
            var encoding = Encoding.UTF8;
            var stream = await SerializeAsync(o, cancellationToken).ConfigureAwait(false);
            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(JsonContentType) { CharSet = encoding.BodyName };
            return content;
        }

        private async Task<T?> DeserializeAsync<T>(string? mediaType, Stream stream, CancellationToken cancellationToken)
        {
            using (stream)
            {
                if (typeof(Stream).IsAssignableFrom(typeof(T))) return (T)(object)stream;

                // if the stream is empty return the default
                if (stream.Length == 0) return default;

                // if content type is provided, it must match JSON
                if (!string.IsNullOrWhiteSpace(mediaType)
                    && !SupportedContentTypes.Contains(value: mediaType, comparer: StringComparer.OrdinalIgnoreCase))
                    return default;

                return await JsonSerializer.DeserializeAsync<T>(utf8Json: stream,
                                                                options: Options.SerializerOptions,
                                                                cancellationToken: cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task<Stream> SerializeAsync<T>(T input, CancellationToken cancellationToken)
        {
            var payload = new MemoryStream();
            await JsonSerializer.SerializeAsync(utf8Json: payload,
                                                value: input,
                                                options: Options.SerializerOptions,
                                                cancellationToken: cancellationToken).ConfigureAwait(false);

            // make the produced payload readable
            payload.Position = 0;
            return payload;
        }

        #endregion
    }
}
