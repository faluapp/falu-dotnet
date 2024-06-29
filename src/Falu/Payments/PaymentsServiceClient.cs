using Falu.Core;
using System.Net.Http.Json;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Payments;

///
public class PaymentsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<Payment>(backChannel, options),
                                                                                        ISupportsListing<Payment, PaymentsListOptions>,
                                                                                        ISupportsRetrieving<Payment>,
                                                                                        ISupportsCreation<Payment, PaymentCreateOptions>,
                                                                                        ISupportsUpdating<Payment, PaymentUpdateOptions>
{
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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Payment>> GetAsync(string id,
                                                            RequestOptions? requestOptions = null,
                                                            CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Create a payment.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Payment>> CreateAsync(PaymentCreateOptions options,
                                                               RequestOptions? requestOptions = null,
                                                               CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.PaymentCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Update a payment.
    /// </summary>
    /// <param name="id">Unique identifier for the payment</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Payment>> UpdateAsync(string id,
                                                               PaymentUpdateOptions options,
                                                               RequestOptions? requestOptions = null,
                                                               CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.PaymentUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }
}
