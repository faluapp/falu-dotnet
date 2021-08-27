using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.PaymentAuthorizations
{
    ///
    public class PaymentAuthorizationsService : BaseService
    {
        ///
        public PaymentAuthorizationsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List payment authorizations.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<PaymentAuthorization>>> ListAsync(PaymentAuthorizationsListOptions? options = null,
                                                                                          RequestOptions? requestOptions = null,
                                                                                          CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/payment_authorizations{query}");
            return await GetAsJsonAsync<List<PaymentAuthorization>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentAuthorization>> GetAsync(string id,
                                                                                   RequestOptions? options = null,
                                                                                   CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/payment_authorizations/{id}");
            return await GetAsJsonAsync<PaymentAuthorization>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentAuthorization>> UpdateAsync(string id,
                                                                                      JsonPatchDocument<PaymentAuthorizationPatchModel> patch,
                                                                                      RequestOptions? options = null,
                                                                                      CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/payment_authorizations/{id}");
            return await PatchAsJsonAsync<PaymentAuthorization>(uri, patch, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Approve a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="model">Update details for the payment authorization</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentAuthorization>> ApproveAsync(string id,
                                                                                       PaymentAuthorizationPatchModel? model = null,
                                                                                       RequestOptions? options = null,
                                                                                       CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            model ??= new PaymentAuthorizationPatchModel();
            var uri = new Uri(BaseAddress, $"/v1/payment_authorizations/{id}/approve");
            return await PostAsync<PaymentAuthorization>(uri, model, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Decline a payment authorization.
        /// </summary>
        /// <param name="id">Unique identifier for the payment authorization</param>
        /// <param name="model">Update details for the payment authorization</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentAuthorization>> DeclineAsync(string id,
                                                                                       PaymentAuthorizationPatchModel? model = null,
                                                                                       RequestOptions? options = null,
                                                                                       CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            model ??= new PaymentAuthorizationPatchModel();
            var uri = new Uri(BaseAddress, $"/v1/payment_authorizations/{id}/decline");
            return await PostAsync<PaymentAuthorization>(uri, model, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
