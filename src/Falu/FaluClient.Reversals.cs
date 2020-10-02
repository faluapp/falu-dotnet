using Falu.Infrastructure;
using Falu.Payments;
using Falu.Payments.Reversals;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu
{
    public partial class FaluClient
    {
        /// <summary>
        /// List payment reversals.
        /// </summary>
        /// <param name="from">Starting date for the payment reversals</param>
        /// <param name="count">Maximum number of items to return</param>
        /// <param name="continuationToken">The continuation token from a previous request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<List<PaymentReversal>>> ListPaymentReversalsAsync(DateTimeOffset? from = null,
                                                                                             int? count = null,
                                                                                             string continuationToken = null,
                                                                                             CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            if (from != null) args["from"] = $"{from:o}";
            if (count != null) args["count"] = $"{count}";
            if (!string.IsNullOrWhiteSpace(continuationToken)) args["ct"] = continuationToken;

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/payments/reversals{query}");
            return await GetAsJsonAsync<List<PaymentReversal>>(uri, cancellationToken);
        }

        /// <summary>
        /// Retrieve a payment reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the payment reversal</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<PaymentReversal>> GetPaymentReversalAsync(string id,
                                                                                     CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/payments/reversals/{id}");
            return await GetAsJsonAsync<PaymentReversal>(uri, cancellationToken);
        }

        /// <summary>
        /// Initiate reversal for a payment.
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<PaymentReversal>> ReversePaymentAsync(PaymentReversalRequest reversal,
                                                                                 CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/payments/reversals");
            return await PostAsJsonAsync<PaymentReversal>(uri, reversal, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update a payment reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the payment reversal</param>
        /// <param name="patch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<PaymentReversal>> UpdatePaymentReversalAsync(string id,
                                                                                        JsonPatchDocument<PaymentReversalPatchModel> patch,
                                                                                        CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/payments/reversals/{id}");
            return await PatchAsJsonAsync<PaymentReversal>(uri, patch, cancellationToken: cancellationToken);
        }
    }
}
