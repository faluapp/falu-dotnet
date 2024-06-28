using Falu.Core;
using System.Net.Http.Json;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.TemporaryKeys;

///
public class TemporaryKeysServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<TemporaryKey>(backChannel, options),
                                                                                             ISupportsListing<TemporaryKey, TemporaryKeysListOptions>,
                                                                                             ISupportsRetrieving<TemporaryKey>
{
    /// <inheritdoc/>
    protected override string BasePath => "/v1/temporary_keys";

    /// <summary>List temporary keys.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<TemporaryKey>>> ListAsync(TemporaryKeysListOptions? options = null,
                                                                        RequestOptions? requestOptions = null,
                                                                        CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List temporary keys recursively.</summary>
    /// <inheritdoc/>
    public IAsyncEnumerable<TemporaryKey> ListRecursivelyAsync(TemporaryKeysListOptions? options = null,
                                                               RequestOptions? requestOptions = null,
                                                               CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a temporary key.
    /// </summary>
    /// <param name="id">Unique identifier for the temporary key</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TemporaryKey>> GetAsync(string id,
                                                                 RequestOptions? requestOptions = null,
                                                                 CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Create a temporary key.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TemporaryKey>> CreateAsync(TemporaryKeyCreateOptions options,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.TemporaryKeyCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Delete a temporary key.
    /// </summary>
    /// <param name="id">Unique identifier for the temporary key</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                              RequestOptions? requestOptions = null,
                                                              CancellationToken cancellationToken = default)
    {
        return DeleteResourceAsync(id, null, requestOptions, cancellationToken);
    }
}
