using Falu.Core;
using Sc = Falu.Serialization.FaluSerializerContext;

namespace Falu.Events;

///
public class EventsServiceClient : BaseServiceClient<WebhookEvent>,
                                   ISupportsListing<WebhookEvent, EventsListOptions>,
                                   ISupportsRetrieving<WebhookEvent>
{
    ///
    public EventsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/events";

    /// <summary>
    /// Retrieve events.
    /// </summary>
    /// <param name="options">Options for filtering and pagination.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<List<WebhookEvent<T>>>> ListAsync<T>(EventsListOptions? options = null,
                                                                              RequestOptions? requestOptions = null,
                                                                              CancellationToken cancellationToken = default)
        where T : class
    {
        var uri = MakePathWithQuery(null, options);
        var jsonTypeInfo = Sc.Default.GetRequriedTypeInfo<List<WebhookEvent<T>>>();
        return RequestAsync(uri, HttpMethod.Get, jsonTypeInfo, null, requestOptions, cancellationToken);
    }

    /// <summary>Retrieve events.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<WebhookEvent>>> ListAsync(EventsListOptions? options = null,
                                                                        RequestOptions? requestOptions = null,
                                                                        CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>Retrieve events recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<WebhookEvent> ListRecursivelyAsync(EventsListOptions? options = null,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>Retrieve events recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<WebhookEvent<T>> ListRecursivelyAsync<T>(EventsListOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
    {
        var jsonTypeInfo = Sc.Default.GetRequriedTypeInfo<List<WebhookEvent<T>>>();
        return ListResourcesRecursivelyAsync(jsonTypeInfo, options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Get an event.
    /// </summary>
    /// <param name="id">Unique identifier for the event</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<WebhookEvent<T>>> GetAsync<T>(string id,
                                                                       RequestOptions? options = null,
                                                                       CancellationToken cancellationToken = default)
        where T : class
    {
        var uri = MakeResourcePath(id);
        var jsonTypeInfo = Sc.Default.GetRequriedTypeInfo<WebhookEvent<T>>();
        return RequestAsync(uri, HttpMethod.Get, jsonTypeInfo, null, options, cancellationToken);
    }

    /// <summary>
    /// Get an event.
    /// </summary>
    /// <param name="id">Unique identifier for the event</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<WebhookEvent>> GetAsync(string id,
                                                                 RequestOptions? options = null,
                                                                 CancellationToken cancellationToken = default)
    {
        var uri = MakeResourcePath(id);
        return RequestResourceAsync(uri, HttpMethod.Get, null, options, cancellationToken);
    }
}
