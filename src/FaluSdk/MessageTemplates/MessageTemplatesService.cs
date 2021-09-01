using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.MessageTemplates
{
    ///
    public class MessageTemplatesService : BaseService
    {
        ///
        public MessageTemplatesService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List message templates.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<MessageTemplate>>> ListAsync(BasicListOptions? options = null,
                                                                                     RequestOptions? requestOptions = null,
                                                                                     CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = $"/v1/message_templates{query}";
            return await GetResourceAsync<List<MessageTemplate>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplate>> GetAsync(string id,
                                                                              RequestOptions? options = null,
                                                                              CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = $"/v1/message_templates/{id}";
            return await GetResourceAsync<MessageTemplate>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a message template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplate>> CreateAsync(MessageTemplatePatchModel template,
                                                                                 RequestOptions? options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (template is null) throw new ArgumentNullException(nameof(template));

            var uri = "/v1/message_templates";
            return await PostAsync<MessageTemplate>(uri, template, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplate>> UpdateAsync(string id,
                                                                                 JsonPatchDocument<MessageTemplatePatchModel> patch,
                                                                                 RequestOptions? options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = $"/v1/message_templates/{id}";
            return await PatchAsync<MessageTemplate>(uri, patch, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<object>> DeleteAsync(string id,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = $"/v1/message_templates/{id}";
            return await DeleteAsync(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Validate a message template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplateValidationResponse>> ValidateAsync(MessageTemplateValidationRequest template,
                                                                                                     RequestOptions? options = null,
                                                                                                     CancellationToken cancellationToken = default)
        {
            if (template is null) throw new ArgumentNullException(nameof(template));

            template.Model?.GetType().EnsureAllowedForMessageTemplateModel();

            var uri = "/v1/message_templates/validate";
            return await PostAsync<MessageTemplateValidationResponse>(uri, template, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
