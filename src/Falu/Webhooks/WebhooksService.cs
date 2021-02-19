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
        /// <param name="count">Maximum number of items to return</param>
        /// <param name="continuationToken">The continuation token from a previous request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<WebhookEndpoint>>> ListAsync(int? count = null,
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
        public virtual async Task<ResourceResponse<WebhookEndpoint>> GetAsync(string id,
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
        public virtual async Task<ResourceResponse<WebhookEndpoint>> CreateAsync(WebhookEndpointPatchModel endpoint,
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
        public virtual async Task<ResourceResponse<WebhookEndpoint>> UpdateAsync(string id,
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
        public virtual async Task<ResourceResponse<WebhookEndpoint>> DeleteAsync(string id,
                                                                                 CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/webhooks/endpoints/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<WebhookEndpoint>(request, cancellationToken);
        }
    }
}
