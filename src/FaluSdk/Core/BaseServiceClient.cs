using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Core;

///
public abstract class BaseServiceClient // This class exists because not all service clients may be based on a resource
{
    ///
    protected internal static string DefaultJsonContentType { get; } = "application/json";

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
            error = await ReadFromJsonAsync(response.Content, SC.Default.FaluError, cancellationToken).ConfigureAwait(false);
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
    {
        var response = await RequestCoreAsync(request, options, cancellationToken).ConfigureAwait(false);

        // if the response was a success then deserialize the body as TResource otherwise TError
        var resource = response.IsSuccessStatusCode
            ? await ReadFromJsonAsync(response.Content, jsonTypeInfo, cancellationToken).ConfigureAwait(false)
            : default;
        var error = !response.IsSuccessStatusCode
            ? await ReadFromJsonAsync(response.Content, SC.Default.FaluError, cancellationToken).ConfigureAwait(false)
            : default;

        return new ResourceResponse<TResource>(response: response, resource: resource, error: error);
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    protected virtual async Task<ResourceResponse<TResource>> RequestCoreAsync<TResource>(HttpRequestMessage request,
                                                                                          RequestOptions? options = null,
                                                                                          JsonSerializerOptions? serializerOptions = null,
                                                                                          CancellationToken cancellationToken = default)
    {
        var response = await RequestCoreAsync(request, options, cancellationToken).ConfigureAwait(false);

        // if the response was a success then deserialize the body as TResource otherwise TError
        var resource = response.IsSuccessStatusCode
            ? await ReadFromJsonAsync<TResource>(response.Content, serializerOptions, cancellationToken).ConfigureAwait(false)
            : default;
        var error = !response.IsSuccessStatusCode
            ? await ReadFromJsonAsync(response.Content, SC.Default.FaluError, cancellationToken).ConfigureAwait(false)
            : default;

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
        request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(DefaultJsonContentType)); // ensure we only get JSON content back
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
    protected virtual Task<T?> ReadFromJsonAsync<T>(HttpContent content,
                                                    JsonTypeInfo<T> jsonTypeInfo,
                                                    CancellationToken cancellationToken = default)
    {
        return content.Headers.ContentLength > 0
            ? content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken)
            : Task.FromResult<T?>(default);
    }

    ///
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    protected virtual Task<T?> ReadFromJsonAsync<T>(HttpContent content,
                                                    JsonSerializerOptions? serializerOptions = null,
                                                    CancellationToken cancellationToken = default)
    {
        return content.Headers.ContentLength > 0
            ? content.ReadFromJsonAsync<T>(serializerOptions, cancellationToken)
            : Task.FromResult<T?>(default);
    }

    #endregion
}
