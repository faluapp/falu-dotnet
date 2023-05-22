using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.MessageBatches;

///
public class MessageBatchesServiceClient : BaseServiceClient<MessageBatch>,
                                           ISupportsListing<MessageBatch, MessageBatchesListOptions>,
                                           ISupportsRetrieving<MessageBatch>,
                                           ISupportsCreation<MessageBatch, MessageBatchCreateRequest>,
                                           ISupportsUpdating<MessageBatch, MessageBatchPatchModel>,
                                           ISupportsCanceling<MessageBatch>,
                                           ISupportsRedaction<MessageBatch>
{
    ///
    public MessageBatchesServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/message_batches";

    /// <summary>List message batches.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<MessageBatch>>> ListAsync(MessageBatchesListOptions? options = null,
                                                                        RequestOptions? requestOptions = null,
                                                                        CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List message batches recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<MessageBatch> ListRecursivelyAsync(MessageBatchesListOptions? options = null,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a message batch.
    /// </summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageBatch>> GetAsync(string id,
                                                                 RequestOptions? options = null,
                                                                 CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>Create a message batch.</summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>You can send up to 1,000 messages in one API request.</remarks>
    public virtual Task<ResourceResponse<MessageBatch>> CreateAsync(MessageBatchCreateRequest request,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var content = FaluJsonContent.Create(request, SC.Default.MessageBatchCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>Update a message batch.</summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageBatch>> UpdateAsync(string id,
                                                                    JsonPatchDocument<MessageBatchPatchModel> request,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var content = FaluJsonContent.Create(request, SC.Default.JsonPatchDocumentMessageBatchPatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
    }

    /// <summary>
    /// Retrieve a message batch status.
    /// </summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageBatchStatus>> StatusAsync(string id,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/status";
        return RequestAsync(uri, HttpMethod.Get, SC.Default.MessageBatchStatus, null, options, cancellationToken);
    }

    /// <summary>Cancel a message batch preventing further updates.</summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<MessageBatch>> CancelAsync(string id,
                                                            RequestOptions? options = null,
                                                            CancellationToken cancellationToken = default)
    {
        return CancelResourceAsync(id, null, options, cancellationToken);
    }

    /// <summary>Redact a message batch to remove all collected information from Falu.</summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<MessageBatch>> RedactAsync(string id,
                                                            RequestOptions? options = null,
                                                            CancellationToken cancellationToken = default)
    {
        return RedactResourceAsync(id, null, options, cancellationToken);
    }
}
