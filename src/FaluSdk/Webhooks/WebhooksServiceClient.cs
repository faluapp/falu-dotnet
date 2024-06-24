using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Webhooks;

///
public class WebhooksServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<WebhookEndpoint>(backChannel, options),
                                                                                        ISupportsListing<WebhookEndpoint, WebhookEndpointsListOptions>,
                                                                                        ISupportsRetrieving<WebhookEndpoint>,
                                                                                        ISupportsCreation<WebhookEndpoint, WebhookEndpointCreateOptions>,
                                                                                        ISupportsUpdating<WebhookEndpoint, WebhookEndpointUpdateOptions>
{
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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<WebhookEndpoint>> GetAsync(string id,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Create a webhook endpoint.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<WebhookEndpoint>> CreateAsync(WebhookEndpointCreateOptions options,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.WebhookEndpointCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Update a webhook endpoint.
    /// </summary>
    /// <param name="id">Unique identifier for the webhook endpoint</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<WebhookEndpoint>> UpdateAsync(string id,
                                                                       JsonPatchDocument<WebhookEndpointUpdateOptions> options,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.JsonPatchDocumentWebhookEndpointUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Delete a webhook endpoint.
    /// </summary>
    /// <param name="id">Unique identifier for the webhook endpoint.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                              RequestOptions? requestOptions = null,
                                                              CancellationToken cancellationToken = default)
    {
        return DeleteResourceAsync(id, null, requestOptions, cancellationToken);
    }
}
