using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Identity
{
    ///
    public class IdentityService : BaseService
    {
        ///
        public IdentityService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// Search for an entity's identity.
        /// </summary>
        /// <param name="search">The details to use for searching.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<IdentityRecord>> SearchAsync(IdentitySearchModel search,
                                                                                RequestOptions options = null,
                                                                                CancellationToken cancellationToken = default)
        {
            if (search is null) throw new ArgumentNullException(nameof(search));

            var uri = new Uri(BaseAddress, "/v1/identity/search");
            return await PostAsJsonAsync<IdentityRecord>(uri, search, options, cancellationToken);
        }

        /// <summary>
        /// Fetch restricted identity data for marketing purposes.
        /// Sensitive data is excluded in the response. The corresponsing properties will be null.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<MarketingResult>>> MarketingAsync(MarketingListOptions options = null,
                                                                                          RequestOptions requestOptions = null,
                                                                                          CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/identity/marketing{query}");
            return await PostAsJsonAsync<List<MarketingResult>>(uri, options, requestOptions, cancellationToken);
        }
    }
}
