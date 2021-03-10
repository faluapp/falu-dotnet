using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Events
{
    ///
    public class EventsService : BaseService
    {
        ///
        public EventsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// Retrieve events.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<WebhookEvent<T>>>> ListAsync<T>(BasicListOptions options = null,
                                                                                        RequestOptions requestOptions = null,
                                                                                        CancellationToken cancellationToken = default)
            where T : class
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/events{query}");
            return await GetAsJsonAsync<List<WebhookEvent<T>>>(uri, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve events.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<WebhookEvent>>> ListAsync(BasicListOptions options = null,
                                                                                  RequestOptions requestOptions = null,
                                                                                  CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/events{query}");
            return await GetAsJsonAsync<List<WebhookEvent>>(uri, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Get an event.
        /// </summary>
        /// <param name="id">Unique identifier for the event</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEvent<T>>> GetAsync<T>(string id,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/events/{id}");
            return await GetAsJsonAsync<WebhookEvent<T>>(uri, options, cancellationToken);
        }

        /// <summary>
        /// Get an event.
        /// </summary>
        /// <param name="id">Unique identifier for the event</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEvent>> GetAsync(string id,
                                                                           RequestOptions options = null,
                                                                           CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/events/{id}");
            return await GetAsJsonAsync<WebhookEvent>(uri, options, cancellationToken);
        }
    }
}
