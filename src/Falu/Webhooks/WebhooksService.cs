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
    public class WebhooksService : BaseService
    {
        ///
        public WebhooksService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List webhook endpoints.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<WebhookEndpoint>>> ListAsync(BasicListOptions options = null,
                                                                                     RequestOptions requestOptions = null, 
                                                                                     CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints{query}");
            return await GetAsJsonAsync<List<WebhookEndpoint>>(uri, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEndpoint>> GetAsync(string id,
                                                                              RequestOptions options = null, 
                                                                              CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints/{id}");
            return await GetAsJsonAsync<WebhookEndpoint>(uri, options, cancellationToken);
        }

        /// <summary>
        /// Create a webhook endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEndpoint>> CreateAsync(WebhookEndpointPatchModel endpoint,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/webhooks/endpoints");
            return await PostAsJsonAsync<WebhookEndpoint>(uri, endpoint, options, cancellationToken);
        }

        /// <summary>
        /// Update a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEndpoint>> UpdateAsync(string id,
                                                                                 JsonPatchDocument<WebhookEndpointPatchModel> patch,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints/{id}");
            return await PatchAsJsonAsync<WebhookEndpoint>(uri, patch, options, cancellationToken);
        }

        /// <summary>
        /// Delete a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEndpoint>> DeleteAsync(string id,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<WebhookEndpoint>(request, options, cancellationToken);
        }
    }
}
