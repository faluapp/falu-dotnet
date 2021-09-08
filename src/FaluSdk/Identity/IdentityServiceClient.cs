using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Identity
{
    ///
    public class IdentityServiceClient : BaseServiceClient<IdentityRecord>
    {
        ///
        public IdentityServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/identity";

        /// <summary>
        /// Search for an entity's identity.
        /// </summary>
        /// <param name="search">The details to use for searching.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<IdentityRecord>> SearchAsync(IdentitySearchModel search,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            var uri = MakePath("/search");
            return RequestAsync<IdentityRecord>(uri, HttpMethod.Post, search, options, cancellationToken);
        }

        /// <summary>
        /// Fetch restricted identity data for marketing purposes.
        /// Sensitive data is excluded in the response. The corresponsing properties will be null.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<MarketingResult>>> MarketingAsync(MarketingListOptions? options = null,
                                                                                    RequestOptions? requestOptions = null,
                                                                                    CancellationToken cancellationToken = default)
        {
            var uri = MakePathWithQuery("/marketing", options);
            return RequestAsync<List<MarketingResult>>(uri, HttpMethod.Post, new { }, requestOptions, cancellationToken);
        }
    }
}
