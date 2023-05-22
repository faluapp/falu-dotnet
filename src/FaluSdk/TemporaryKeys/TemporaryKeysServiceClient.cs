using Falu.Core;
using SC = Falu.Serialization.FaluSerializerContext;

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
        var content = MakeJsonHttpContent(request, SC.Default.TemporaryKeyCreateRequest);
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
