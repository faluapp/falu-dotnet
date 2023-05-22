using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Webhooks;

///
public class WebhooksServiceClient : BaseServiceClient<WebhookEndpoint>,
                                     ISupportsListing<WebhookEndpoint, WebhookEndpointsListOptions>,
                                     ISupportsRetrieving<WebhookEndpoint>,
                                     ISupportsCreation<WebhookEndpoint, WebhookEndpointCreateRequest>,
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
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<WebhookEndpoint>> CreateAsync(WebhookEndpointCreateRequest request,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.WebhookEndpointCreateRequest, cancellationToken).ConfigureAwait(false);
        return await CreateResourceAsync(content, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Update a webhook endpoint.
    /// </summary>
    /// <param name="id">Unique identifier for the webhook endpoint</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<WebhookEndpoint>> UpdateAsync(string id,
                                                                             JsonPatchDocument<WebhookEndpointPatchModel> request,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.JsonPatchDocumentWebhookEndpointPatchModel, cancellationToken).ConfigureAwait(false);
        return await UpdateResourceAsync(id, content, options, cancellationToken).ConfigureAwait(false);
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
