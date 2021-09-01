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
    public class PaymentRefundsService : BaseService<PaymentRefund>
    {
        ///
        public PaymentRefundsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/payment_refunds";

        /// <summary>
        /// List payment refunds.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<PaymentRefund>>> ListAsync(PaymentRefundsListOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a payment refund.
        /// </summary>
        /// <param name="id">Unique identifier for the payment refund.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<PaymentRefund>> GetAsync(string id,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
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

            var uri = "/v1/payment_reversals";
            return await PostAsync<PaymentRefund>(uri, reversal, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a payment refund.
        /// </summary>
        /// <param name="id">Unique identifier for the payment refund.</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<PaymentRefund>> UpdateAsync(string id,
                                                                         JsonPatchDocument<PaymentRefundPatchModel> patch,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }
    }
}
