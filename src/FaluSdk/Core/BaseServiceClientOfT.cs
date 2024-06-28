using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace Falu.Core;

///
public abstract class BaseServiceClient<TResource>(HttpClient backChannel,
                                                   FaluClientOptions options,
                                                   JsonTypeInfo<TResource> jsonTypeInfo,
                                                   JsonTypeInfo<List<TResource>>? listJsonTypeInfo = null) : BaseServiceClient(backChannel, options)
{
    ///
    protected BaseServiceClient(HttpClient backChannel, FaluClientOptions options)
        : this(backChannel,
               options,
               Serialization.FaluSerializerContext.Default.GetRequiredTypeInfo<TResource>(),
               Serialization.FaluSerializerContext.Default.GetTypeInfo<List<TResource>>())
    { }

    ///
    protected abstract string BasePath { get; }

    ///
    protected virtual JsonTypeInfo<TResource> JsonTypeInfo { get; } = jsonTypeInfo ?? throw new ArgumentNullException(nameof(jsonTypeInfo));

    ///
    protected virtual JsonTypeInfo<List<TResource>>? ListJsonTypeInfo { get; } = listJsonTypeInfo;


    ///
    protected virtual Task<ResourceResponse<TResource>> GetResourceAsync(string id,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
    {
        var uri = MakeResourcePath(id);
        return RequestResourceAsync(uri, HttpMethod.Get, null, options, cancellationToken);
    }

    ///
    protected virtual async Task<ResourceResponse<List<T>>> ListResourcesAsync<T>(JsonTypeInfo<List<T>> jsonTypeInfo,
                                                                                  BasicListOptions? options = null,
                                                                                  RequestOptions? requestOptions = null,
                                                                                  CancellationToken cancellationToken = default)
    {
        var uri = MakePathWithQuery(null, options);
        return await RequestAsync(uri, HttpMethod.Get, jsonTypeInfo, null, requestOptions, cancellationToken).ConfigureAwait(false);
    }

    ///
    protected virtual Task<ResourceResponse<List<TResource>>> ListResourcesAsync(BasicListOptions? options = null,
                                                                                 RequestOptions? requestOptions = null,
                                                                                 CancellationToken cancellationToken = default)
    {
        if (ListJsonTypeInfo is null) throw new InvalidOperationException("ListJsonTypeInfo is null.");
        return ListResourcesAsync(ListJsonTypeInfo, options, requestOptions, cancellationToken);
    }

    ///
    protected virtual Task<ResourceResponse<TResource>> CreateResourceAsync(HttpContent content,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));

        var uri = MakePath();
        return RequestResourceAsync(uri, HttpMethod.Post, content, options, cancellationToken);
    }

    ///
    protected virtual Task<ResourceResponse<TResource>> UpdateResourceAsync(string id,
                                                                            HttpContent content,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));

        var uri = MakeResourcePath(id);
        return RequestResourceAsync(uri, HttpMethod.Patch, content, options, cancellationToken);
    }


    ///
    protected virtual Task<ResourceResponse<TResource>> CancelResourceAsync(string id,
                                                                            HttpContent? content = null,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/cancel";
        return RequestResourceAsync(uri, HttpMethod.Post, content, options, cancellationToken);
    }

    ///
    protected virtual Task<ResourceResponse<TResource>> RedactResourceAsync(string id,
                                                                            HttpContent? content = null,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/redact";
        return RequestResourceAsync(uri, HttpMethod.Post, content, options, cancellationToken);
    }

    ///
    protected virtual Task<ResourceResponse<object>> DeleteResourceAsync(string id,
                                                                         HttpContent? content = null,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
    {
        var uri = MakeResourcePath(id);
        return RequestAsync(uri, HttpMethod.Delete, content, options, cancellationToken);
    }

    #region List Recursively

    ///
    protected async IAsyncEnumerable<T> ListResourcesRecursivelyAsync<T>(JsonTypeInfo<List<T>> jsonTypeInfo,
                                                                         BasicListOptions? options,
                                                                         RequestOptions? requestOptions,
                                                                         [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        // cloning prevents the underlying values from being changed while we iterate
        options = options is null ? new BasicListOptions() : options with { };

        do
        {
            cancellationToken.ThrowIfCancellationRequested();

            // list next batch
            var response = await ListResourcesAsync(jsonTypeInfo: jsonTypeInfo,
                                                    options: options,
                                                    requestOptions: requestOptions,
                                                    cancellationToken: cancellationToken).ConfigureAwait(false);

            // ensure the request succeeded
            response.EnsureSuccess();

            // produce results for the batch
            var items = response.Resource!;
            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // We do not expect null but for safety, skip them
                if (item is null) continue;

                yield return item;
            }

            // if there are no more results, break the loop
            if (response.HasMoreResults != true)
            {
                break;
            }

            // set the continuation token for the next batch request
            // nothing else should be changed in the options so that
            // the continuation token works as expected
            options.ContinuationToken = response.ContinuationToken;
        } while (true);
    }

    ///
    protected IAsyncEnumerable<TResource> ListResourcesRecursivelyAsync(BasicListOptions? options,
                                                                        RequestOptions? requestOptions,
                                                                        CancellationToken cancellationToken = default)
    {
        if (ListJsonTypeInfo is null) throw new InvalidOperationException("ListJsonTypeInfo is null.");
        return ListResourcesRecursivelyAsync(ListJsonTypeInfo, options, requestOptions, cancellationToken);
    }

    #endregion

    ///
    protected virtual Task<ResourceResponse<TResource>> RequestResourceAsync(string uri,
                                                                             HttpMethod method,
                                                                             HttpContent? content = null,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
    {
        return RequestAsync(uri, method, JsonTypeInfo, content, options, cancellationToken);
    }

    #region Helpers

    /// <summary>Generate a path.</summary>
    /// <param name="subPath">The sub path to add at the end.</param>
    /// <returns>The generated path.</returns>
    protected virtual string MakePath(string? subPath = null) => $"{BasePath}{subPath}";

    /// <summary>Generate path for a resource.</summary>
    /// <param name="id">Unique identifier of the resource.</param>
    /// <returns>The path for the resource.</returns>
    protected virtual string MakeResourcePath(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
        }

        return $"{MakePath()}/{id}";
    }

    /// <summary>Combine path and query.</summary>
    /// <param name="subPath">The sub path to add before the query.</param>
    /// <param name="options">The options to generate keys and values for the query.</param>
    /// <returns>The path and query combined.</returns>
    protected virtual string MakePathWithQuery(string? subPath, BasicListOptions? options)
    {
        var args = new QueryValues();
        options?.Populate(args);

        var query = args.ToString();
        return MakePathWithQuery(subPath: subPath, query: query);
    }

    /// <summary>Combine path and query.</summary>
    /// <param name="subPath">The sub path to add before the query.</param>
    /// <param name="query">The query to append.</param>
    /// <returns>The path and query combined.</returns>
    protected virtual string MakePathWithQuery(string? subPath, string? query)
    {
        var path = MakePath(subPath);
        if (string.IsNullOrWhiteSpace(query)) return path;

        if (!query.StartsWith('?')) query = $"?{query}";
        return $"{path}{query}";
    }

    #endregion

}
