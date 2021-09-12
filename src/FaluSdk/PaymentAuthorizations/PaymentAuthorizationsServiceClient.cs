using Falu.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.PaymentAuthorizations
{
    ///
    public class PaymentAuthorizationsServiceClient : BaseServiceClient<PaymentAuthorization>, ISupportsListing<PaymentAuthorization, PaymentAuthorizationsListOptions>
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

        /// <summary>
        /// Retrieve a payment authorization.
        /// </summary>
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

        /// <summary>
        /// Update a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<PaymentAuthorization>> UpdateAsync(string id,
                                                                                JsonPatchDocument<PaymentAuthorizationPatchModel> patch,
                                                                                RequestOptions? options = null,
                                                                                CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }

        /// <summary>
        /// Approve a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="options">Update details for the payment authorization</param>
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
            return RequestAsync<PaymentAuthorization>(uri, HttpMethod.Post, options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Decline a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="options">Update details for the payment authorization</param>
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
            return RequestAsync<PaymentAuthorization>(uri, HttpMethod.Post, options, requestOptions, cancellationToken);
        }
    }
}
