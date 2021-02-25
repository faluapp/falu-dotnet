using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Messages
{
    ///
    public class MessagesService : BaseService
    {
        ///
        public MessagesService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List messages.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Message>>> ListAsync(MessagesListOptions options,
                                                                             RequestOptions requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/messages{query}");
            return await GetAsJsonAsync<List<Message>>(uri, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Message>> GetAsync(string id,
                                                                      RequestOptions options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/messages/{id}");
            return await GetAsJsonAsync<Message>(uri, options, cancellationToken);
        }

        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Message>> CreateAsync(MessageCreateRequest message,
                                                                         RequestOptions options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/messages");
            return await PostAsJsonAsync<Message>(uri, message, options, cancellationToken);
        }

        /// <summary>
        /// Update a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Message>> UpdateAsync(string id,
                                                                         JsonPatchDocument<MessagePatchModel> patch,
                                                                         RequestOptions options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/messages/{id}");
            return await PatchAsJsonAsync<Message>(uri, patch, options, cancellationToken);
        }

        /// <summary>
        /// Send a batch of messages.
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Message>>> CreateBatchAsync(IEnumerable<MessageCreateRequest> messages,
                                                                                    RequestOptions options = null,
                                                                                    CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/messages/bulk");
            return await PostAsJsonAsync<List<Message>>(uri, messages, options, cancellationToken);
        }
    }
}
