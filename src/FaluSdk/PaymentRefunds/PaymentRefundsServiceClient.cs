﻿using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.PaymentRefunds;

///
public class PaymentRefundsServiceClient : BaseServiceClient<PaymentRefund>,
                                           ISupportsListing<PaymentRefund, PaymentRefundsListOptions>,
                                           ISupportsRetrieving<PaymentRefund>,
                                           ISupportsCreation<PaymentRefund, PaymentRefundCreateRequest>,
                                           ISupportsUpdating<PaymentRefund, PaymentRefundPatchModel>
{
    ///
    public PaymentRefundsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

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
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentRefund>> CreateAsync(PaymentRefundCreateRequest request,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.PaymentRefundCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>
    /// Update a payment refund.
    /// </summary>
    /// <param name="id">Unique identifier for the payment refund.</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentRefund>> UpdateAsync(string id,
                                                                     JsonPatchDocument<PaymentRefundPatchModel> request,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.JsonPatchDocumentPaymentRefundPatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
    }
}
