using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Messages;

///
public class MessagesServiceClient : BaseServiceClient<Message>,
                                     ISupportsListing<Message, MessagesListOptions>,
                                     ISupportsRetrieving<Message>,
                                     //ISupportsCreation<Message, MessageCreateRequest>,
                                     ISupportsUpdating<Message, MessagePatchModel>,
                                     ISupportsCanceling<Message>,
                                     ISupportsRedaction<Message>
{
    ///
    public MessagesServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/messages";

    /// <summary>List messages.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<Message>>> ListAsync(MessagesListOptions? options = null,
                                                                   RequestOptions? requestOptions = null,
                                                                   CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List messages recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<Message> ListRecursivelyAsync(MessagesListOptions? options = null,
                                                                  RequestOptions? requestOptions = null,
                                                                  CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a message.
    /// </summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> GetAsync(string id,
                                                            RequestOptions? options = null,
                                                            CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>Create a message.</summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> CreateAsync(MessageCreateRequest request,
                                                               RequestOptions? options = null,
                                                               CancellationToken cancellationToken = default)
    {
        var content = MakeJsonHttpContent(request, SC.Default.MessageCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>Update a message.</summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> UpdateAsync(string id,
                                                               JsonPatchDocument<MessagePatchModel> request,
                                                               RequestOptions? options = null,
                                                               CancellationToken cancellationToken = default)
    {
        var content = MakeJsonHttpContent(request, SC.Default.JsonPatchDocumentMessagePatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
    }

    /// <summary>Cancel a message preventing further updates.</summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<Message>> CancelAsync(string id,
                                                       RequestOptions? options = null,
                                                       CancellationToken cancellationToken = default)
    {
        return CancelResourceAsync(id, null, options, cancellationToken);
    }

    /// <summary>Redact a message to remove all collected information from Falu.</summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<Message>> RedactAsync(string id,
                                                       RequestOptions? options = null,
                                                       CancellationToken cancellationToken = default)
    {
        return RedactResourceAsync(id, null, options, cancellationToken);
    }
}
