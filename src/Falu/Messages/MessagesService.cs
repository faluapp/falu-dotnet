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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Message>>> ListAsync(MessagesListOptions options,
                                                                             CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/messages{query}");
            return await GetAsJsonAsync<List<Message>>(uri, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Message>> GetAsync(string id,
                                                                      CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/messages/{id}");
            return await GetAsJsonAsync<Message>(uri, cancellationToken);
        }

        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Message>> CreateAsync(MessageCreateRequest message,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/messages");
            return await PostAsJsonAsync<Message>(uri, message, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="patch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Message>> UpdateAsync(string id,
                                                                         JsonPatchDocument<MessagePatchModel> patch,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/messages/{id}");
            return await PatchAsJsonAsync<Message>(uri, patch, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Send a batch of messages.
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Message>>> CreateBatchAsync(IEnumerable<MessageCreateRequest> messages,
                                                                                    CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, "/v1/messages/bulk");
            return await PostAsJsonAsync<List<Message>>(uri, messages, cancellationToken: cancellationToken);
        }
    }
}
