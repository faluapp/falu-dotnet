using Falu.Core;
using System.Net.Http.Json;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.MessageBatches;

///
public class MessageBatchesServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<MessageBatch>(backChannel, options),
                                                                                              ISupportsListing<MessageBatch, MessageBatchesListOptions>,
                                                                                              ISupportsRetrieving<MessageBatch>,
                                                                                              ISupportsCreation<MessageBatch, MessageBatchCreateOptions>,
                                                                                              ISupportsUpdating<MessageBatch, MessageBatchUpdateOptions>,
                                                                                              ISupportsCanceling<MessageBatch>,
                                                                                              ISupportsRedaction<MessageBatch>
{
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
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>You can send up to 1,000 messages in one API request.</remarks>
    public virtual Task<ResourceResponse<MessageBatch>> CreateAsync(MessageBatchCreateOptions options,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.MessageBatchCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>Update a message batch.</summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageBatch>> UpdateAsync(string id,
                                                                    MessageBatchUpdateOptions options,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.MessageBatchUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a message batch status.
    /// </summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageBatchStatus>> StatusAsync(string id,
                                                                          RequestOptions? requestOptions = null,
                                                                          CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/status";
        return RequestAsync(uri, HttpMethod.Get, SC.Default.MessageBatchStatus, null, requestOptions, cancellationToken);
    }

    /// <summary>Cancel a message batch preventing further updates.</summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<MessageBatch>> CancelAsync(string id,
                                                            RequestOptions? requestOptions = null,
                                                            CancellationToken cancellationToken = default)
    {
        return CancelResourceAsync(id, null, requestOptions, cancellationToken);
    }

    /// <summary>Redact a message batch to remove all collected information from Falu.</summary>
    /// <param name="id">Unique identifier for the message batch.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<MessageBatch>> RedactAsync(string id,
                                                            RequestOptions? requestOptions = null,
                                                            CancellationToken cancellationToken = default)
    {
        return RedactResourceAsync(id, null, requestOptions, cancellationToken);
    }
}
