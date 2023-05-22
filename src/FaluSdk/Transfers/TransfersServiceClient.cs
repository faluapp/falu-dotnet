using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Transfers;

///
public class TransfersServiceClient : BaseServiceClient<Transfer>,
                                      ISupportsListing<Transfer, TransfersListOptions>,
                                      ISupportsRetrieving<Transfer>,
                                      ISupportsCreation<Transfer, TransferCreateRequest>,
                                      ISupportsUpdating<Transfer, TransferPatchModel>
{
    ///
    public TransfersServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/transfers";

    /// <summary>List transfers.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<Transfer>>> ListAsync(TransfersListOptions? options = null,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List transfers recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<Transfer> ListRecursivelyAsync(TransfersListOptions? options = null,
                                                                   RequestOptions? requestOptions = null,
                                                                   CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a transfer.
    /// </summary>
    /// <param name="id">Unique identifier for the transfer</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Transfer>> GetAsync(string id,
                                                             RequestOptions? options = null,
                                                             CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create a transfer.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<Transfer>> CreateAsync(TransferCreateRequest request,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.TransferCreateRequest, cancellationToken).ConfigureAwait(false);
        return await CreateResourceAsync(content, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Update a transfer.
    /// </summary>
    /// <param name="id">Unique identifier for the transfer</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<Transfer>> UpdateAsync(string id,
                                                                      JsonPatchDocument<TransferPatchModel> request,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.JsonPatchDocumentTransferPatchModel, cancellationToken).ConfigureAwait(false);
        return await UpdateResourceAsync(id, content, options, cancellationToken).ConfigureAwait(false);
    }
}
