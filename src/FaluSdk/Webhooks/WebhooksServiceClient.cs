using Falu.Core;
using Tingle.Extensions.JsonPatch;

namespace Falu.Webhooks;

///
public class WebhooksServiceClient : BaseServiceClient<WebhookEndpoint>,
                                     ISupportsListing<WebhookEndpoint, WebhookEndpointsListOptions>,
                                     ISupportsRetrieving<WebhookEndpoint>,
                                     ISupportsUpdating<WebhookEndpoint, WebhookEndpointPatchModel>
{
    ///
    public WebhooksServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/webhooks/endpoints";

    /// <summary>List webhook endpoints.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<WebhookEndpoint>>> ListAsync(WebhookEndpointsListOptions? options = null,
                                                                           RequestOptions? requestOptions = null,
                                                                           CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List webhook endpoints recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<WebhookEndpoint> ListRecursivelyAsync(WebhookEndpointsListOptions? options = null,
                                                                          RequestOptions? requestOptions = null,
                                                                          CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
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
