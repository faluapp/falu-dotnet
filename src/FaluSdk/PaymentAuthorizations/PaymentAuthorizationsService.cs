using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.PaymentAuthorizations
{
    ///
    public class PaymentAuthorizationsService : BaseService<PaymentAuthorization>, ISupportsListing<PaymentAuthorization, PaymentAuthorizationsListOptions>
    {
        ///
        public PaymentAuthorizationsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/payment_authorizations";

        /// <summary>
        /// List payment authorizations.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<PaymentAuthorization>>> ListAsync(PaymentAuthorizationsListOptions? options = null,
                                                                                    RequestOptions? requestOptions = null,
                                                                                    CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
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
        /// <param name="model">Update details for the payment authorization</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<PaymentAuthorization>> ApproveAsync(string id,
                                                                                 PaymentAuthorizationPatchModel? model = null,
                                                                                 RequestOptions? options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            var uri = $"{MakeResourcePath(id)}/approve";
            model ??= new PaymentAuthorizationPatchModel();
            return RequestAsync<PaymentAuthorization>(uri, HttpMethod.Post, model, options, cancellationToken);
        }

        /// <summary>
        /// Decline a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="model">Update details for the payment authorization</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<PaymentAuthorization>> DeclineAsync(string id,
                                                                                 PaymentAuthorizationPatchModel? model = null,
                                                                                 RequestOptions? options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            var uri = $"{MakeResourcePath(id)}/decline";
            model ??= new PaymentAuthorizationPatchModel();
            return RequestAsync<PaymentAuthorization>(uri, HttpMethod.Post, model, options, cancellationToken);
        }
    }
}
