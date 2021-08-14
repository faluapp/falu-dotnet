using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.PaymentRefunds
{
    ///
    public class PaymentRefundsService : BaseService
    {
        ///
        public PaymentRefundsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List payment refunds.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<PaymentRefund>>> ListAsync(PaymentRefundsListOptions? options = null,
                                                                                   RequestOptions? requestOptions = null,
                                                                                   CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/payment_reversals{query}");
            return await GetAsJsonAsync<List<PaymentRefund>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a payment refund.
        /// </summary>
        /// <param name="id">Unique identifier for the payment refund.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentRefund>> GetAsync(string id,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/payment_reversals/{id}");
            return await GetAsJsonAsync<PaymentRefund>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create payment refund.
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentRefund>> CreateAsync(PaymentRefundRequest reversal,
                                                                               RequestOptions? options = null,
                                                                               CancellationToken cancellationToken = default)
        {
            if (reversal is null) throw new ArgumentNullException(nameof(reversal));

            var uri = new Uri(BaseAddress, "/v1/payment_reversals");
            return await PostAsJsonAsync<PaymentRefund>(uri, reversal, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a payment refund.
        /// </summary>
        /// <param name="id">Unique identifier for the payment refund.</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentRefund>> UpdateAsync(string id,
                                                                               JsonPatchDocument<PaymentRefundPatchModel> patch,
                                                                               RequestOptions? options = null,
                                                                               CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/payment_reversals/{id}");
            return await PatchAsJsonAsync<PaymentRefund>(uri, patch, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
