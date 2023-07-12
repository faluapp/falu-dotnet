using Falu.Core;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.TemporaryKeys;

///
public class TemporaryKeysServiceClient : BaseServiceClient<TemporaryKey>,
                                          ISupportsListing<TemporaryKey, TemporaryKeysListOptions>,
                                          ISupportsRetrieving<TemporaryKey>
{
    ///
    public TemporaryKeysServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

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
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TemporaryKey>> GetAsync(string id,
                                                                 RequestOptions? options = null,
                                                                 CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create a temporary key.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TemporaryKey>> CreateAsync(TemporaryKeyCreateRequest request,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var content = FaluJsonContent.Create(request, SC.Default.TemporaryKeyCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>
    /// Delete a temporary key.
    /// </summary>
    /// <param name="id">Unique identifier for the temporary key</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                              RequestOptions? options = null,
                                                              CancellationToken cancellationToken = default)
    {
        return DeleteResourceAsync(id, null, options, cancellationToken);
    }
}
