using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Payments
{
    ///
    public class PaymentsService : BaseService
    {
        ///
        public PaymentsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List payments.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Payment>>> ListAsync(BasicListOptions options = null,
                                                                             CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/payments{query}");
            return await GetAsJsonAsync<List<Payment>>(uri, cancellationToken);
        }

        /// <summary>
        /// Retrieve a payment.
        /// </summary>
        /// <param name="id">Unique identifier for the payment</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Payment>> GetAsync(string id,
                                                                      CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/payments/{id}");
            return await GetAsJsonAsync<Payment>(uri, cancellationToken);
        }

        /// <summary>
        /// Initiate a payment.
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Payment>> CreateAsync(PaymentRequest payment,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/payments");
            return await PostAsJsonAsync<Payment>(uri, payment, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update a payment.
        /// </summary>
        /// <param name="id">Unique identifier for the payment</param>
        /// <param name="patch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Payment>> UpdateAsync(string id,
                                                                         JsonPatchDocument<PaymentPatchModel> patch,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/payments/{id}");
            return await PatchAsJsonAsync<Payment>(uri, patch, cancellationToken: cancellationToken);
        }
    }
}
