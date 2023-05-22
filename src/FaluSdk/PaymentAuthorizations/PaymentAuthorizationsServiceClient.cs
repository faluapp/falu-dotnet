using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.PaymentAuthorizations;

///
public class PaymentAuthorizationsServiceClient : BaseServiceClient<PaymentAuthorization>,
                                                  ISupportsListing<PaymentAuthorization, PaymentAuthorizationsListOptions>,
                                                  ISupportsRetrieving<PaymentAuthorization>,
                                                  ISupportsUpdating<PaymentAuthorization, PaymentAuthorizationPatchModel>
{
    ///
    public PaymentAuthorizationsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

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
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> GetAsync(string id,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>Update a payment authorization.</summary>
    /// <param name="id">Unique identifier for the payment authorization</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> UpdateAsync(string id,
                                                                            JsonPatchDocument<PaymentAuthorizationPatchModel> request,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
    {
        var content = FaluJsonContent.Create(request, SC.Default.JsonPatchDocumentPaymentAuthorizationPatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
    }

    /// <summary>Approve a payment authorization.</summary>
    /// <param name="id">Unique identifier for the payment authorization.</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> ApproveAsync(string id,
                                                                             PaymentAuthorizationApproveOptions? request = null,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/approve";
        request ??= new PaymentAuthorizationApproveOptions();
        var content = FaluJsonContent.Create(request, SC.Default.PaymentAuthorizationApproveOptions);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.PaymentAuthorization, content, options, cancellationToken);
    }

    /// <summary>Decline a payment authorization.</summary>
    /// <param name="id">Unique identifier for the payment authorization.</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<PaymentAuthorization>> DeclineAsync(string id,
                                                                             PaymentAuthorizationDeclineOptions? request = null,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/decline";
        request ??= new PaymentAuthorizationDeclineOptions();
        var content = FaluJsonContent.Create(request, SC.Default.PaymentAuthorizationDeclineOptions);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.PaymentAuthorization, content, options, cancellationToken);
    }
}
