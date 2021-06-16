using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.MessageStreams
{
    ///
    public class MessageStreamsService : BaseService
    {
        ///
        public MessageStreamsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List message streams.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<MessageStream>>> ListAsync(BasicListOptions options = null,
                                                                                   RequestOptions requestOptions = null,
                                                                                   CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/message_streams{query}");
            return await GetAsJsonAsync<List<MessageStream>>(uri, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageStream>> GetAsync(string id,
                                                                            RequestOptions options = null,
                                                                            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/message_streams/{id}");
            return await GetAsJsonAsync<MessageStream>(uri, options, cancellationToken);
        }

        /// <summary>
        /// Create a message stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageStream>> CreateAsync(MessageStreamCreateModel stream,
                                                                               RequestOptions options = null,
                                                                               CancellationToken cancellationToken = default)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            var uri = new Uri(BaseAddress, "/v1/message_streams");
            return await PostAsJsonAsync<MessageStream>(uri, stream, options, cancellationToken);
        }

        /// <summary>
        /// Update a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageStream>> UpdateAsync(string id,
                                                                               JsonPatchDocument<MessageStreamPatchModel> patch,
                                                                               RequestOptions options = null,
                                                                               CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/message_streams/{id}");
            return await PatchAsJsonAsync<MessageStream>(uri, patch, options, cancellationToken);
        }

        /// <summary>
        /// Delete a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageStream>> DeleteAsync(string id,
                                                                          RequestOptions options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/message_streams/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<MessageStream>(request, options, cancellationToken);
        }
    }
}
