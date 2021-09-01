using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Events
{
    ///
    public class EventsService : BaseService<WebhookEvent>, ISupportsListing<WebhookEvent, BasicListOptions> // TODO: add custom listing options
    {
        ///
        public EventsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/events";

        /// <summary>
        /// Retrieve events.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<WebhookEvent<T>>>> ListAsync<T>(BasicListOptions? options = null,
                                                                                  RequestOptions? requestOptions = null,
                                                                                  CancellationToken cancellationToken = default)
            where T : class
        {
            return ListResourcesAsync<WebhookEvent<T>>(options, requestOptions, cancellationToken);
        }

        /// <summary>Retrieve events.</summary>
        /// <inheritdoc/>
        public virtual Task<ResourceResponse<List<WebhookEvent>>> ListAsync(BasicListOptions? options = null,
                                                                            RequestOptions? requestOptions = null,
                                                                            CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>Retrieve events recursively.</summary>
        /// <inheritdoc/>
        public virtual IAsyncEnumerable<WebhookEvent> ListRecursivelyAsync(BasicListOptions? options = null,
                                                                           RequestOptions? requestOptions = null,
                                                                           CancellationToken cancellationToken = default)
        {
            return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
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
            return GetResourceAsync<WebhookEvent<T>>(id, options, cancellationToken);
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
            return GetResourceAsync(id, options, cancellationToken);
        }
    }
}
