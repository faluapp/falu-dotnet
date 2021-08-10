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
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Payment>>> ListAsync(PaymentsListOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/payments{query}");
            return await GetAsJsonAsync<List<Payment>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a payment.
        /// </summary>
        /// <param name="id">Unique identifier for the payment</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Payment>> GetAsync(string id,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/payments/{id}");
            return await GetAsJsonAsync<Payment>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a payment.
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Payment>> CreateAsync(PaymentRequest payment,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            if (payment is null) throw new ArgumentNullException(nameof(payment));

            var uri = new Uri(BaseAddress, "/v1/payments");
            return await PostAsJsonAsync<Payment>(uri, payment, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a payment.
        /// </summary>
        /// <param name="id">Unique identifier for the payment</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Payment>> UpdateAsync(string id,
                                                                         JsonPatchDocument<PaymentPatchModel> patch,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/payments/{id}");
            return await PatchAsJsonAsync<Payment>(uri, patch, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
