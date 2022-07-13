using Falu.Core;
using Tingle.Extensions.JsonPatch;

namespace Falu.Payments;

///
public class PaymentsServiceClient : BaseServiceClient<Payment>,
                                     ISupportsListing<Payment, PaymentsListOptions>,
                                     ISupportsRetrieving<Payment>,
                                     ISupportsCreation<Payment, PaymentCreateRequest>,
                                     ISupportsUpdating<Payment, PaymentPatchModel>
{
    ///
    public PaymentsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/payments";

    /// <summary>List payments.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<Payment>>> ListAsync(PaymentsListOptions? options = null,
                                                                   RequestOptions? requestOptions = null,
                                                                   CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List payments recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<Payment> ListRecursivelyAsync(PaymentsListOptions? options = null,
                                                                  RequestOptions? requestOptions = null,
                                                                  CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a payment.
    /// </summary>
    /// <param name="id">Unique identifier for the payment</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Payment>> GetAsync(string id,
                                                            RequestOptions? options = null,
                                                            CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create a payment.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Payment>> CreateAsync(PaymentCreateRequest request,
                                                               RequestOptions? options = null,
                                                               CancellationToken cancellationToken = default)
    {
        return CreateResourceAsync(request, options, cancellationToken);
    }

    /// <summary>
    /// Update a payment.
    /// </summary>
    /// <param name="id">Unique identifier for the payment</param>
    /// <param name="patch"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Payment>> UpdateAsync(string id,
                                                               JsonPatchDocument<PaymentPatchModel> patch,
                                                               RequestOptions? options = null,
                                                               CancellationToken cancellationToken = default)
    {
        return UpdateResourceAsync(id, patch, options, cancellationToken);
    }
}
