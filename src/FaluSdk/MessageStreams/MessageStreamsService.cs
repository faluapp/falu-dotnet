using Falu.Core;
using Falu.Infrastructure;
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
        public virtual Task<ResourceResponse<List<MessageStream>>> ListAsync(BasicListOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageStream>> GetAsync(string id,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Create a message stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageStream>> CreateAsync(MessageStreamCreateRequest stream,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(stream, options, cancellationToken);
        }

        /// <summary>
        /// Update a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageStream>> UpdateAsync(string id,
                                                                         JsonPatchDocument<MessageStreamPatchModel> patch,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
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


        /// <summary>
        /// Archive a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageStream>> ArchiveAsync(string id,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            var uri = $"{MakeResourcePath(id)}/archive";
            return RequestAsync<MessageStream>(uri, HttpMethod.Post, new { }, options, cancellationToken);
        }

        /// <summary>
        /// Unarchive a message stream.
        /// </summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageStream>> UnarchiveAsync(string id,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            var uri = $"{MakeResourcePath(id)}/unarchive";
            return RequestAsync<MessageStream>(uri, HttpMethod.Post, new { }, options, cancellationToken);
        }
    }
}
