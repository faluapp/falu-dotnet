using Falu.Core;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.MessageSuppressions;

///
public class MessageSuppressionsServiceClient : BaseServiceClient<MessageSuppression>,
                                                ISupportsListing<MessageSuppression, MessageSuppressionsListOptions>,
                                                ISupportsRetrieving<MessageSuppression>,
                                                ISupportsCreation<MessageSuppression, MessageSuppressionCreateRequest>
{
    ///
    public MessageSuppressionsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/message_suppressions";

    /// <summary>List message suppressions.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<MessageSuppression>>> ListAsync(MessageSuppressionsListOptions? options = null,
                                                                              RequestOptions? requestOptions = null,
                                                                              CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List message suppressions recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<MessageSuppression> ListRecursivelyAsync(MessageSuppressionsListOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a message suppression.
    /// </summary>
    /// <param name="id">Unique identifier for the message suppression</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageSuppression>> GetAsync(string id,
                                                                       RequestOptions? options = null,
                                                                       CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create a message suppression.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageSuppression>> CreateAsync(MessageSuppressionCreateRequest request,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
    {
        var content = MakeJsonHttpContent(request, SC.Default.MessageSuppressionCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>
    /// Delete a message suppression.
    /// </summary>
    /// <param name="id">Unique identifier for the message suppression</param>
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
