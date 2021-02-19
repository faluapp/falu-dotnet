using Falu.Infrastructure;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Payments
{
    ///
    public class PaymentsBalanceService : BaseService
    {
        ///
        public PaymentsBalanceService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// Retrieve balance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<AccountBalance>> GetAsync(CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/payments/balance");
            return await GetAsJsonAsync<AccountBalance>(uri, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Force a balance refresh.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<object>> RefreshAsync(CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/payments/balance/refresh");
            return await PostAsJsonAsync<object>(uri, new { }, cancellationToken: cancellationToken);
        }
    }
}
