using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.MessageTemplates;

///
public class MessageTemplatesServiceClient : BaseServiceClient<MessageTemplate>,
                                             ISupportsListing<MessageTemplate, MessageTemplatesListOptions>,
                                             ISupportsRetrieving<MessageTemplate>,
                                             ISupportsCreation<MessageTemplate, MessageTemplateCreateRequest>,
                                             ISupportsUpdating<MessageTemplate, MessageTemplatePatchModel>
{
    ///
    public MessageTemplatesServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/message_templates";

    /// <summary>List message templates.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<MessageTemplate>>> ListAsync(MessageTemplatesListOptions? options = null,
                                                                           RequestOptions? requestOptions = null,
                                                                           CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List message templates recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<MessageTemplate> ListRecursivelyAsync(MessageTemplatesListOptions? options = null,
                                                                          RequestOptions? requestOptions = null,
                                                                          CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a message template.
    /// </summary>
    /// <param name="id">Unique identifier for the message template</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageTemplate>> GetAsync(string id,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create a message template.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<MessageTemplate>> CreateAsync(MessageTemplateCreateRequest request,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.MessageTemplateCreateRequest, cancellationToken).ConfigureAwait(false);
        return await CreateResourceAsync(content, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Update a message template.
    /// </summary>
    /// <param name="id">Unique identifier for the message template</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<MessageTemplate>> UpdateAsync(string id,
                                                                             JsonPatchDocument<MessageTemplatePatchModel> request,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.JsonPatchDocumentMessageTemplatePatchModel, cancellationToken).ConfigureAwait(false);
        return await UpdateResourceAsync(id, content, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a message template.
    /// </summary>
    /// <param name="id">Unique identifier for the message template</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                              RequestOptions? options = null,
                                                              CancellationToken cancellationToken = default)
    {
        return DeleteResourceAsync(id, null, options, cancellationToken);
    }

    /// <summary>
    /// Validate a message template.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<MessageTemplateValidationResponse>> ValidateAsync(MessageTemplateValidationRequest request,
                                                                                                 RequestOptions? options = null,
                                                                                                 CancellationToken cancellationToken = default)
    {
        var uri = MakePath("/validate");
        var content = await MakeJsonHttpContentAsync(request, SC.Default.MessageTemplateValidationRequest, cancellationToken).ConfigureAwait(false);
        return await RequestAsync(uri, HttpMethod.Post, SC.Default.MessageTemplateValidationResponse, content, options, cancellationToken).ConfigureAwait(false);
    }
}
