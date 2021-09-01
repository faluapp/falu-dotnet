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
    public class MessagesService : BaseService<Message>
    {
        ///
        public MessagesService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/messages";

        /// <summary>
        /// List messages.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<Message>>> ListAsync(MessagesListOptions? options = null,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Message>> GetAsync(string id,
                                                                RequestOptions? options = null,
                                                                CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Message>> CreateAsync(MessageCreateRequest message,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));
            message.Template?.Model?.GetType().EnsureAllowedForMessageTemplateModel();

            var uri = "/v1/messages";
            return await PostAsync<Message>(uri, message, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a message.
        /// </summary>
        /// <param name="id">Unique identifier for the message</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Message>> UpdateAsync(string id,
                                                                   JsonPatchDocument<MessagePatchModel> patch,
                                                                   RequestOptions? options = null,
                                                                   CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }

        /// <summary>
        /// Send a batch of messages.
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Message>>> CreateBatchAsync(IList<MessageCreateRequest> messages,
                                                                                    RequestOptions? options = null,
                                                                                    CancellationToken cancellationToken = default)
        {
            if (messages is null) throw new ArgumentNullException(nameof(messages));

            if (messages.Count > 10_000)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(messages),
                                                      message: "The service does not support more than 10,000 (10k) messages");
            }

            foreach(var m in messages)
            {
                m.Template?.Model?.GetType().EnsureAllowedForMessageTemplateModel();
            }

            var uri = "/v1/messages/bulk";
            return await PostAsync<List<Message>>(uri, messages, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
