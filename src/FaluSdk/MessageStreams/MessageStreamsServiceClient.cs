using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.MessageStreams;

///
public class MessageStreamsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<MessageStream>(backChannel, options),
                                                                                              ISupportsListing<MessageStream, MessageStreamsListOptions>,
                                                                                              ISupportsRetrieving<MessageStream>,
                                                                                              ISupportsCreation<MessageStream, MessageStreamCreateOptions>,
                                                                                              ISupportsUpdating<MessageStream, MessageStreamUpdateOptions>
{
    /// <inheritdoc/>
    protected override string BasePath => "/v1/message_streams";

    /// <summary>List message streams.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<MessageStream>>> ListAsync(MessageStreamsListOptions? options = null,
                                                                         RequestOptions? requestOptions = null,
                                                                         CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List message streams recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<MessageStream> ListRecursivelyAsync(MessageStreamsListOptions? options = null,
                                                                        RequestOptions? requestOptions = null,
                                                                        CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> GetAsync(string id,
                                                                  RequestOptions? options = null,
                                                                  CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create a message stream.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> CreateAsync(MessageStreamCreateOptions options,
                                                                     RequestOptions? requestOptions = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.MessageStreamCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Update a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> UpdateAsync(string id,
                                                                     JsonPatchDocument<MessageStreamUpdateOptions> options,
                                                                     RequestOptions? requestOptions = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.JsonPatchDocumentMessageStreamUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Delete a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                              RequestOptions? requestOptions = null,
                                                              CancellationToken cancellationToken = default)
    {
        return DeleteResourceAsync(id, null, requestOptions, cancellationToken);
    }


    /// <summary>
    /// Archive a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> ArchiveAsync(string id,
                                                                      MessageStreamArchiveOptions options,
                                                                      RequestOptions? requestOptions = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/archive";
        var content = JsonContent.Create(options, SC.Default.MessageStreamArchiveOptions);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.MessageStream, content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Unarchive a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> UnarchiveAsync(string id,
                                                                        MessageStreamUnarchiveOptions options,
                                                                        RequestOptions? requestOptions = null,
                                                                        CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/unarchive";
        var content = JsonContent.Create(options, SC.Default.MessageStreamUnarchiveOptions);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.MessageStream, content, requestOptions, cancellationToken);
    }
}
