using Falu.Core;
using Tingle.Extensions.JsonPatch;

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
    /// <param name="message"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> CreateAsync(MessageCreateRequest message,
                                                               RequestOptions? options = null,
                                                               CancellationToken cancellationToken = default)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        message.Template?.Model?.GetType().EnsureAllowedForMessageTemplateModel();

        return CreateResourceAsync<Message>(message, options, cancellationToken);
    }

    /// <summary>Update a message.</summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="patch"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> UpdateAsync(string id,
                                                               JsonPatchDocument<MessagePatchModel> patch,
                                                               RequestOptions? options = null,
                                                               CancellationToken cancellationToken = default)
    {
        return UpdateResourceAsync(id, patch, options, cancellationToken);
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
        return CancelResourceAsync(id, options, cancellationToken);
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
        return RedactResourceAsync(id, options, cancellationToken);
    }
}
