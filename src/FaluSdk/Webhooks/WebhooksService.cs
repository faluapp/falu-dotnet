using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Webhooks
{
    ///
    public class WebhooksService : BaseService<WebhookEndpoint>
    {
        ///
        public WebhooksService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/webhooks/endpoints";

        /// <summary>
        /// List webhook endpoints.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<WebhookEndpoint>>> ListAsync(BasicListOptions? options = null,
                                                                                     RequestOptions? requestOptions = null, 
                                                                                     CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = $"/v1/webhooks/endpoints{query}";
            return await GetResourceAsync<List<WebhookEndpoint>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<WebhookEndpoint>> GetAsync(string id,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Create a webhook endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEndpoint>> CreateAsync(WebhookEndpointPatchModel endpoint,
                                                                                 RequestOptions? options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (endpoint is null) throw new ArgumentNullException(nameof(endpoint));

            var uri = "/v1/webhooks/endpoints";
            return await PostAsync<WebhookEndpoint>(uri, endpoint, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<WebhookEndpoint>> UpdateAsync(string id,
                                                                           JsonPatchDocument<WebhookEndpointPatchModel> patch,
                                                                           RequestOptions? options = null,
                                                                           CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }

        /// <summary>
        /// Delete a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<object>> DeleteAsync(string id,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
        {
            return await DeleteResourceAsync(id, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
