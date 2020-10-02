using Falu.Infrastructure;
using Falu.Webhooks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu
{
    public partial class FaluClient 
    {
        /// <summary>
        /// List webhook endpoints.
        /// </summary>
        /// <param name="count">Maximum number of items to return</param>
        /// <param name="continuationToken">The continuation token from a previous request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<List<WebhookEndpoint>>> ListWebhookEndpointsAsync(int? count = null,
                                                                                             string continuationToken = null,
                                                                                             CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            if (count != null) args["count"] = $"{count}";
            if (!string.IsNullOrWhiteSpace(continuationToken)) args["ct"] = continuationToken;

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints{query}");
            return await GetAsJsonAsync<List<WebhookEndpoint>>(uri, cancellationToken);
        }

        /// <summary>
        /// Retrieve a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<WebhookEndpoint>> GetWebhookEndpointAsync(string id,
                                                                                     CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints/{id}");
            return await GetAsJsonAsync<WebhookEndpoint>(uri, cancellationToken);
        }

        /// <summary>
        /// Create a webhook endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<WebhookEndpoint>> CreateWebhookEndpointAsync(WebhookEndpointPatchModel endpoint,
                                                                                        CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/webhooks/endpoints");
            return await PostAsJsonAsync<WebhookEndpoint>(uri, endpoint, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint</param>
        /// <param name="patch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<WebhookEndpoint>> UpdateWebhookEndpointAsync(string id,
                                                                                        JsonPatchDocument<WebhookEndpointPatchModel> patch,
                                                                                        CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints/{id}");
            return await PatchAsJsonAsync<WebhookEndpoint>(uri, patch, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Delete a webhook endpoint.
        /// </summary>
        /// <param name="id">Unique identifier for the webhook endpoint</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceResponse<WebhookEndpoint>> DeleteWebhookEndpointAsync(string id,
                                                                                        CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<WebhookEndpoint>(request, cancellationToken);
        }
    }
}
