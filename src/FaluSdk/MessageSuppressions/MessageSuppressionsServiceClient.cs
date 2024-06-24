using Falu.Core;
using System.Net.Http.Json;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.MessageSuppressions;

///
///
public class MessageSuppressionsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<MessageSuppression>(backChannel, options),
                                                ISupportsListing<MessageSuppression, MessageSuppressionsListOptions>,
                                                ISupportsRetrieving<MessageSuppression>,
                                                ISupportsCreation<MessageSuppression, MessageSuppressionCreateOptions>
{

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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageSuppression>> GetAsync(string id,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Create a message suppression.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MessageSuppression>> CreateAsync(MessageSuppressionCreateOptions options,
                                                                          RequestOptions? requestOptions = null,
                                                                          CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.MessageSuppressionCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Delete a message suppression.
    /// </summary>
    /// <param name="id">Unique identifier for the message suppression</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                              RequestOptions? requestOptions = null,
                                                              CancellationToken cancellationToken = default)
    {
        return DeleteResourceAsync(id, null, requestOptions, cancellationToken);
    }
}
