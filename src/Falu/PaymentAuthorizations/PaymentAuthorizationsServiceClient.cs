using Falu.Core;
using System.Net.Http.Json;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.PaymentAuthorizations;

///
public class PaymentAuthorizationsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<PaymentAuthorization>(backChannel, options),
                                                                                                     ISupportsListing<PaymentAuthorization, PaymentAuthorizationsListOptions>,
                                                                                                     ISupportsRetrieving<PaymentAuthorization>,
                                                                                                     ISupportsUpdating<PaymentAuthorization, PaymentAuthorizationUpdateOptions>
{
    /// <inheritdoc/>
    protected override string BasePath => "/v1/payment_authorizations";

    /// <summary>List payment authorizations.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<PaymentAuthorization>>> ListAsync(PaymentAuthorizationsListOptions? options = null,
                                                                                RequestOptions? requestOptions = null,
                                                                                CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List payment authorizations recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<PaymentAuthorization> ListRecursivelyAsync(PaymentAuthorizationsListOptions? options = null,
                                                                               RequestOptions? requestOptions = null,
                                                                               CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>Retrieve a payment authorization.</summary>
    /// <param name="id">Unique identifier for the payment authorization</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> GetAsync(string id,
                                                                         RequestOptions? requestOptions = null,
                                                                         CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>Update a payment authorization.</summary>
    /// <param name="id">Unique identifier for the payment authorization</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> UpdateAsync(string id,
                                                                            PaymentAuthorizationUpdateOptions options,
                                                                            RequestOptions? requestOptions = null,
                                                                            CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.PaymentAuthorizationUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }

    /// <summary>Approve a payment authorization.</summary>
    /// <param name="id">Unique identifier for the payment authorization.</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> ApproveAsync(string id,
                                                                             PaymentAuthorizationApproveOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/approve";
        options ??= new PaymentAuthorizationApproveOptions();
        var content = JsonContent.Create(options, SC.Default.PaymentAuthorizationApproveOptions);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.PaymentAuthorization, content, requestOptions, cancellationToken);
    }

    /// <summary>Decline a payment authorization.</summary>
    /// <param name="id">Unique identifier for the payment authorization.</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> DeclineAsync(string id,
                                                                             PaymentAuthorizationDeclineOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/decline";
        options ??= new PaymentAuthorizationDeclineOptions();
        var content = JsonContent.Create(options, SC.Default.PaymentAuthorizationDeclineOptions);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.PaymentAuthorization, content, requestOptions, cancellationToken);
    }
}
