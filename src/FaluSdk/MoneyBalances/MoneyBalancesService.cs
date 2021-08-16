﻿using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Payments
{
    ///
    public class MoneyBalancesService : BaseService
    {
        ///
        public MoneyBalancesService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// Retrieve balance.
        /// </summary>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MoneyBalances>> GetAsync(RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/money_balances");
            return await GetAsJsonAsync<MoneyBalances>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Force a balance refresh.
        /// </summary>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<object>> RefreshAsync(RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/money_balances/refresh");
            return await PostAsJsonAsync<object>(uri, new { }, options, cancellationToken).ConfigureAwait(false);
        }
    }
}