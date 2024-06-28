using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Messages;

///
public class MessagesServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<Message>(backChannel, options),
                                                                                        ISupportsListing<Message, MessagesListOptions>,
                                                                                        ISupportsRetrieving<Message>,
                                                                                        //ISupportsCreation<Message, MessageCreateRequest>,
                                                                                        ISupportsUpdating<Message, MessageUpdateOptions>,
                                                                                        ISupportsCanceling<Message>,
                                                                                        ISupportsRedaction<Message>
{
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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> GetAsync(string id,
                                                            RequestOptions? requestOptions = null,
                                                            CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>Create a message.</summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> CreateAsync(MessageCreateOptions options,
                                                               RequestOptions? requestOptions = null,
                                                               CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.MessageCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>Update a message.</summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Message>> UpdateAsync(string id,
                                                               JsonPatchDocument<MessageUpdateOptions> options,
                                                               RequestOptions? requestOptions = null,
                                                               CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.JsonPatchDocumentMessageUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }

    /// <summary>Cancel a message preventing further updates.</summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<Message>> CancelAsync(string id,
                                                       RequestOptions? requestOptions = null,
                                                       CancellationToken cancellationToken = default)
    {
        return CancelResourceAsync(id, null, requestOptions, cancellationToken);
    }

    /// <summary>Redact a message to remove all collected information from Falu.</summary>
    /// <param name="id">Unique identifier for the message.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<Message>> RedactAsync(string id,
                                                       RequestOptions? requestOptions = null,
                                                       CancellationToken cancellationToken = default)
    {
        return RedactResourceAsync(id, null, requestOptions, cancellationToken);
    }
}
