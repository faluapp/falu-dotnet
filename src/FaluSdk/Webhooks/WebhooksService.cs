using Falu.Core;
using Falu.Infrastructure;
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
        public virtual Task<ResourceResponse<List<WebhookEndpoint>>> ListAsync(BasicListOptions? options = null,
                                                                               RequestOptions? requestOptions = null,
                                                                               CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
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
        public virtual Task<ResourceResponse<WebhookEndpoint>> CreateAsync(WebhookEndpointCreateRequest endpoint,
                                                                           RequestOptions? options = null,
                                                                           CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(endpoint, options, cancellationToken);
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
        public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                                  RequestOptions? options = null,
                                                                  CancellationToken cancellationToken = default)
        {
            return DeleteResourceAsync(id, options, cancellationToken);
        }
    }
}
