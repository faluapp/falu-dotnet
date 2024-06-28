using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.PaymentRefunds;

///
public class PaymentRefundsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<PaymentRefund>(backChannel, options),
                                                                                              ISupportsListing<PaymentRefund, PaymentRefundsListOptions>,
                                                                                              ISupportsRetrieving<PaymentRefund>,
                                                                                              ISupportsCreation<PaymentRefund, PaymentRefundCreateOptions>,
                                                                                              ISupportsUpdating<PaymentRefund, PaymentRefundUpdateOptions>
{
    /// <inheritdoc/>
    protected override string BasePath => "/v1/payment_refunds";

    /// <summary>List payment refunds.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<PaymentRefund>>> ListAsync(PaymentRefundsListOptions? options = null,
                                                                         RequestOptions? requestOptions = null,
                                                                         CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List payment refunds recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<PaymentRefund> ListRecursivelyAsync(PaymentRefundsListOptions? options = null,
                                                                        RequestOptions? requestOptions = null,
                                                                        CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a payment refund.
    /// </summary>
    /// <param name="id">Unique identifier for the payment refund.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentRefund>> GetAsync(string id,
                                                                  RequestOptions? options = null,
                                                                  CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create payment refund.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentRefund>> CreateAsync(PaymentRefundCreateOptions options,
                                                                     RequestOptions? requestOptions = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.PaymentRefundCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Update a payment refund.
    /// </summary>
    /// <param name="id">Unique identifier for the payment refund.</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentRefund>> UpdateAsync(string id,
                                                                     JsonPatchDocument<PaymentRefundUpdateOptions> options,
                                                                     RequestOptions? requestOptions = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.JsonPatchDocumentPaymentRefundUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }
}
