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
    public class MessageStreamsService : BaseService<MessageStream>
    {
        ///
        public MessageStreamsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/message_streams";

        /// <summary>
        /// List message streams.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<MessageStream>>> ListAsync(BasicListOptions? options = null,
                                                                                   RequestOptions? requestOptions = null,
                                                                                   CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = $"/v1/message_streams{query}";
            return await GetResourceAsync<List<MessageStream>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageStream>> GetAsync(string id,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = $"/v1/message_streams/{id}";
            return await GetResourceAsync<MessageStream>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a message stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageStream>> CreateAsync(MessageStreamCreateModel stream,
                                                                               RequestOptions? options = null,
                                                                               CancellationToken cancellationToken = default)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            var uri = "/v1/message_streams";
            return await PostAsync<MessageStream>(uri, stream, options, cancellationToken).ConfigureAwait(false);
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
                                                                               RequestOptions? options = null,
                                                                               CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = $"/v1/message_streams/{id}";
            return await PatchAsync<MessageStream>(uri, patch, options, cancellationToken).ConfigureAwait(false);
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
            return DeleteResourceAsync(id, options, cancellationToken);
        }
    }
}
