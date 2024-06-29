using Falu.Core;
using System.Net.Http.Json;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Transfers;

///
public class TransfersServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<Transfer>(backChannel, options),
                                                                                         ISupportsListing<Transfer, TransfersListOptions>,
                                                                                         ISupportsRetrieving<Transfer>,
                                                                                         ISupportsCreation<Transfer, TransferCreateOptions>,
                                                                                         ISupportsUpdating<Transfer, TransferUpdateOptions>
{
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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Transfer>> GetAsync(string id,
                                                             RequestOptions? requestOptions = null,
                                                             CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Create a transfer.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Transfer>> CreateAsync(TransferCreateOptions options,
                                                                RequestOptions? requestOptions = null,
                                                                CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.TransferCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Update a transfer.
    /// </summary>
    /// <param name="id">Unique identifier for the transfer</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Transfer>> UpdateAsync(string id,
                                                                TransferUpdateOptions options,
                                                                RequestOptions? requestOptions = null,
                                                                CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.TransferUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }
}
