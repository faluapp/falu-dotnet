using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.TransferReversals;

///
public class TransferReversalsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<TransferReversal>(backChannel, options),
                                                                                                 ISupportsListing<TransferReversal, TransferReversalsListOptions>,
                                                                                                 ISupportsRetrieving<TransferReversal>,
                                                                                                 ISupportsCreation<TransferReversal, TransferReversalCreateRequest>,
                                                                                                 ISupportsUpdating<TransferReversal, TransferReversalPatchModel>
{
    /// <inheritdoc/>
    protected override string BasePath => "/v1/transfer_reversals";

    /// <summary>List transfer reversals.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<TransferReversal>>> ListAsync(TransferReversalsListOptions? options = null,
                                                                            RequestOptions? requestOptions = null,
                                                                            CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List transfer reversals recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<TransferReversal> ListRecursivelyAsync(TransferReversalsListOptions? options = null,
                                                                           RequestOptions? requestOptions = null,
                                                                           CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a transfer reversal.
    /// </summary>
    /// <param name="id">Unique identifier for the transfer reversal</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TransferReversal>> GetAsync(string id,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create transfer reversal.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TransferReversal>> CreateAsync(TransferReversalCreateRequest request,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.TransferReversalCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>
    /// Update a transfer reversal.
    /// </summary>
    /// <param name="id">Unique identifier for the transfer reversal</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<TransferReversal>> UpdateAsync(string id,
                                                                        JsonPatchDocument<TransferReversalPatchModel> request,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.JsonPatchDocumentTransferReversalPatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
    }
}
