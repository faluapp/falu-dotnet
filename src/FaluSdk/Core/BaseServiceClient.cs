using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Core;

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
    protected virtual async Task<ResourceResponse<object>> RequestAsync(string uri,
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

        var response = await RequestCoreAsync(request, options, cancellationToken).ConfigureAwait(false);
        var error = default(FaluError);

        // if the response was a success then deserialize the body as TResource otherwise TError
        if (!response.IsSuccessStatusCode)
        {
            error = await ExtractFromResponseBodyAsync(response, SC.Default.FaluError, cancellationToken).ConfigureAwait(false);
        }

        return new ResourceResponse<object>(response: response, resource: null, error: error);
    }

    ///
    protected virtual async Task<ResourceResponse<TResource>> RequestAsync<TResource>(string uri,
                                                                                      HttpMethod method,
                                                                                      JsonTypeInfo<TResource> jsonTypeInfo,
                                                                                      HttpContent? content = null,
                                                                                      RequestOptions? options = null,
                                                                                      CancellationToken cancellationToken = default)
        where TResource : class
    {
        var request = new HttpRequestMessage(method, uri);
        if (content is not null)
        {
            request.Content = content;
        }

        return await RequestCoreAsync(request, jsonTypeInfo, options, cancellationToken).ConfigureAwait(false);
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    protected virtual async Task<ResourceResponse<TResource>> RequestAsync<TResource>(string uri,
                                                                                      HttpMethod method,
                                                                                      HttpContent? content = null,
                                                                                      RequestOptions? options = null,
                                                                                      JsonSerializerOptions? serializerOptions = null,
                                                                                      CancellationToken cancellationToken = default)
        where TResource : class
    {
        var request = new HttpRequestMessage(method, uri);
        if (content is not null)
        {
            request.Content = content;
        }

        return await RequestCoreAsync<TResource>(request, options, serializerOptions, cancellationToken).ConfigureAwait(false);
    }

    ///
    protected virtual async Task<ResourceResponse<TResource>> RequestCoreAsync<TResource>(HttpRequestMessage request,
                                                                                          JsonTypeInfo<TResource> jsonTypeInfo,
                                                                                          RequestOptions? options = null,
                                                                                          CancellationToken cancellationToken = default)
        where TResource : class
    {
        var response = await RequestCoreAsync(request, options, cancellationToken).ConfigureAwait(false);
        var resource = default(TResource);
        var error = default(FaluError);

        // if the response was a success then deserialize the body as TResource otherwise TError
        if (response.IsSuccessStatusCode)
        {
            resource = await ExtractFromResponseBodyAsync(response, jsonTypeInfo, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            error = await ExtractFromResponseBodyAsync(response, SC.Default.FaluError, cancellationToken).ConfigureAwait(false);
        }
        return new ResourceResponse<TResource>(response: response, resource: resource, error: error);
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    protected virtual async Task<ResourceResponse<TResource>> RequestCoreAsync<TResource>(HttpRequestMessage request,
                                                                                          RequestOptions? options = null,
                                                                                          JsonSerializerOptions? serializerOptions = null,
                                                                                          CancellationToken cancellationToken = default)
        where TResource : class
    {
        var response = await RequestCoreAsync(request, options, cancellationToken).ConfigureAwait(false);
        var resource = default(TResource);
        var error = default(FaluError);

        // if the response was a success then deserialize the body as TResource otherwise TError
        if (response.IsSuccessStatusCode)
        {
            resource = await ExtractFromResponseBodyAsync<TResource>(response, serializerOptions, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            error = await ExtractFromResponseBodyAsync(response, SC.Default.FaluError, cancellationToken).ConfigureAwait(false);
        }
        return new ResourceResponse<TResource>(response: response, resource: resource, error: error);
    }

    ///
    protected virtual async Task<T?> ExtractFromResponseBodyAsync<T>(HttpResponseMessage response,
                                                                     JsonTypeInfo<T> jsonTypeInfo,
                                                                     CancellationToken cancellationToken = default)
    {

        // get the content type
        var contentType = response.Content.Headers?.ContentType;

        // get a stream reference for the content
        // the stream may still be being incoming and thus we should only read when necessary
#if NET5_0_OR_GREATER
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#else
        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#endif

        return await DeserializeAsync(contentType?.MediaType, stream, jsonTypeInfo, cancellationToken).ConfigureAwait(false);
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    protected virtual async Task<T?> ExtractFromResponseBodyAsync<T>(HttpResponseMessage response,
                                                                     JsonSerializerOptions? serializerOptions = null,
                                                                     CancellationToken cancellationToken = default)
    {

        // get the content type
        var contentType = response.Content.Headers?.ContentType;

        // get a stream reference for the content
        // the stream may still be being incoming and thus we should only read when necessary
#if NET5_0_OR_GREATER
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#else
        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#endif

        return await DeserializeAsync<T>(contentType?.MediaType, stream, serializerOptions, cancellationToken).ConfigureAwait(false);
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

    ///
    protected async Task<HttpContent> MakeJsonHttpContentAsync<T>(T @object, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken = default)
    {
        var encoding = Encoding.UTF8;
        var stream = await SerializeAsync(@object, jsonTypeInfo, cancellationToken).ConfigureAwait(false);
        var content = new StreamContent(stream);
        content.Headers.ContentType = new MediaTypeHeaderValue(JsonContentType) { CharSet = encoding.BodyName };
        return content;
    }

    private static async Task<T?> DeserializeAsync<T>(string? mediaType, Stream stream, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken = default)
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

            return await JsonSerializer.DeserializeAsync(utf8Json: stream,
                                                         jsonTypeInfo: jsonTypeInfo,
                                                         cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }

    private static async Task<Stream> SerializeAsync<T>(T input, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken = default)
    {
        var payload = new MemoryStream();
        await JsonSerializer.SerializeAsync(utf8Json: payload,
                                            value: input,
                                            jsonTypeInfo: jsonTypeInfo,
                                            cancellationToken: cancellationToken).ConfigureAwait(false);

        // make the produced payload readable
        payload.Position = 0;
        return payload;
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    protected async Task<HttpContent> MakeJsonHttpContentAsync<T>(T @object, JsonSerializerOptions? serializerOptions = null, CancellationToken cancellationToken = default)
    {
        var encoding = Encoding.UTF8;
        var stream = await SerializeAsync(@object, serializerOptions, cancellationToken).ConfigureAwait(false);
        var content = new StreamContent(stream);
        content.Headers.ContentType = new MediaTypeHeaderValue(JsonContentType) { CharSet = encoding.BodyName };
        return content;
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    private static async Task<T?> DeserializeAsync<T>(string? mediaType, Stream stream, JsonSerializerOptions? serializerOptions = null, CancellationToken cancellationToken = default)
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
                                                            options: serializerOptions,
                                                            cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    private static async Task<Stream> SerializeAsync<T>(T input, JsonSerializerOptions? serializerOptions = null, CancellationToken cancellationToken = default)
    {
        var payload = new MemoryStream();
        await JsonSerializer.SerializeAsync(utf8Json: payload,
                                            value: input,
                                            options: serializerOptions,
                                            cancellationToken: cancellationToken).ConfigureAwait(false);

        // make the produced payload readable
        payload.Position = 0;
        return payload;
    }

    #endregion
}
