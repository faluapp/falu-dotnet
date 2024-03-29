﻿using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.MessageStreams;

///
public class MessageStreamsServiceClient : BaseServiceClient<MessageStream>,
                                           ISupportsListing<MessageStream, MessageStreamsListOptions>,
                                           ISupportsRetrieving<MessageStream>,
                                           ISupportsCreation<MessageStream, MessageStreamCreateRequest>,
                                           ISupportsUpdating<MessageStream, MessageStreamPatchModel>
{
    ///
    public MessageStreamsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

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
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> CreateAsync(MessageStreamCreateRequest request,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.MessageStreamCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>
    /// Update a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> UpdateAsync(string id,
                                                                     JsonPatchDocument<MessageStreamPatchModel> request,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.JsonPatchDocumentMessageStreamPatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
    }

    /// <summary>
    /// Delete a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
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
    /// Archive a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> ArchiveAsync(string id,
                                                                      MessageStreamArchiveRequest request,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/archive";
        var content = JsonContent.Create(request, SC.Default.MessageStreamArchiveRequest);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.MessageStream, content, options, cancellationToken);
    }

    /// <summary>
    /// Unarchive a message stream.
    /// </summary>
    /// <param name="id">Unique identifier for the message stream</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageStream>> UnarchiveAsync(string id,
                                                                        MessageStreamUnarchiveRequest request,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/unarchive";
        var content = JsonContent.Create(request, SC.Default.MessageStreamUnarchiveRequest);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.MessageStream, content, options, cancellationToken);
    }
}
