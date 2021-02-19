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
        /// <param name="from">Starting date for the events</param>
        /// <param name="count">Maximum number of items to return</param>
        /// <param name="continuationToken">The continuation token from a previous request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<WebhookEvent<T>>>> ListAsync<T>(DateTimeOffset? from = null,
                                                                                        int? count = null,
                                                                                        string continuationToken = null,
                                                                                        CancellationToken cancellationToken = default)
            where T : class
        {
            var args = new Dictionary<string, string>();
            if (from != null) args["from"] = $"{from:o}";
            if (count != null) args["count"] = $"{count}";
            if (!string.IsNullOrWhiteSpace(continuationToken)) args["ct"] = continuationToken;

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/events{query}");
            return await GetAsJsonAsync<List<WebhookEvent<T>>>(uri, cancellationToken);
        }

        /// <summary>
        /// Retrieve events.
        /// </summary>
        /// <param name="from">Starting date for the events</param>
        /// <param name="count">Maximum number of items to return</param>
        /// <param name="continuationToken">The continuation token from a previous request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<WebhookEvent>>> ListAsync(DateTimeOffset? from = null,
                                                                                  int? count = null,
                                                                                  string continuationToken = null,
                                                                                  CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            if (from != null) args["from"] = $"{from:o}";
            if (count != null) args["count"] = $"{count}";
            if (!string.IsNullOrWhiteSpace(continuationToken)) args["ct"] = continuationToken;

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/events{query}");
            return await GetAsJsonAsync<List<WebhookEvent>>(uri, cancellationToken);
        }

        /// <summary>
        /// Get an event.
        /// </summary>
        /// <param name="id">Unique identifier for the event</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEvent<T>>> GetAsync<T>(string id,
                                                                                 CancellationToken cancellationToken = default)
            where T : class
        {
            var uri = new Uri(BaseAddress, $"/v1/events/{id}");
            return await GetAsJsonAsync<WebhookEvent<T>>(uri, cancellationToken);
        }

        /// <summary>
        /// Get an event.
        /// </summary>
        /// <param name="id">Unique identifier for the event</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<WebhookEvent>> GetAsync(string id,
                                                                           CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/events/{id}");
            return await GetAsJsonAsync<WebhookEvent>(uri, cancellationToken);
        }
    }
}
