using Falu.Core;

namespace Falu.TemporaryKeys;

///
public class TemporaryKeysServiceClient : BaseServiceClient<TemporaryKey>
{
    ///
    public TemporaryKeysServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/temporary_keys";

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
        return CreateResourceAsync(request, options, cancellationToken);
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
        return DeleteResourceAsync(id, options, cancellationToken);
    }
}
