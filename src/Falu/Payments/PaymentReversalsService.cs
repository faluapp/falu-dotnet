using Falu.Core;
using Falu.Infrastructure;
using Falu.Payments.Reversals;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Payments
{
    ///
    public class PaymentReversalsService : BaseService
    {
        ///
        public PaymentReversalsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List payment reversals.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="reqeustOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<PaymentReversal>>> ListAsync(BasicListOptions options = null,
                                                                                     RequestOptions reqeustOptions = null,
                                                                                     CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/payment_reversals{query}");
            return await GetAsJsonAsync<List<PaymentReversal>>(uri, reqeustOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a payment reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the payment reversal</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentReversal>> GetAsync(string id,
                                                                              RequestOptions options = null,
                                                                              CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/payment_reversals/{id}");
            return await GetAsJsonAsync<PaymentReversal>(uri, options, cancellationToken);
        }

        /// <summary>
        /// Create payment reversal.
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentReversal>> CreateAsync(PaymentReversalRequest reversal,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (reversal is null) throw new ArgumentNullException(nameof(reversal));

            var uri = new Uri(BaseAddress, "/v1/payment_reversals");
            return await PostAsJsonAsync<PaymentReversal>(uri, reversal, options, cancellationToken);
        }

        /// <summary>
        /// Update a payment reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the payment reversal</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<PaymentReversal>> UpdateAsync(string id,
                                                                                 JsonPatchDocument<ReversalPatchModel> patch,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/payment_reversals/{id}");
            return await PatchAsJsonAsync<PaymentReversal>(uri, patch, options, cancellationToken);
        }
    }
}
