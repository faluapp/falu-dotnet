using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Webhooks;

///
public class WebhooksServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<WebhookEndpoint>(backChannel, options),
                                                                                        ISupportsListing<WebhookEndpoint, WebhookEndpointsListOptions>,
                                                                                        ISupportsRetrieving<WebhookEndpoint>,
                                                                                        ISupportsCreation<WebhookEndpoint, WebhookEndpointCreateRequest>,
                                                                                        ISupportsUpdating<WebhookEndpoint, WebhookEndpointPatchModel>
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
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<WebhookEndpoint>> CreateAsync(WebhookEndpointCreateRequest request,
                                                                       RequestOptions? options = null,
                                                                       CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.WebhookEndpointCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>
    /// Update a webhook endpoint.
    /// </summary>
    /// <param name="id">Unique identifier for the webhook endpoint</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<WebhookEndpoint>> UpdateAsync(string id,
                                                                       JsonPatchDocument<WebhookEndpointPatchModel> request,
                                                                       RequestOptions? options = null,
                                                                       CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.JsonPatchDocumentWebhookEndpointPatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
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
        return DeleteResourceAsync(id, null, options, cancellationToken);
    }
}
