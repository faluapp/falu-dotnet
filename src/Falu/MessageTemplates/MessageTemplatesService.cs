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
        public virtual async Task<ResourceResponse<List<MessageTemplate>>> ListAsync(BasicListOptions options = null,
                                                                                     RequestOptions requestOptions = null,
                                                                                     CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/message_templates{query}");
            return await GetAsJsonAsync<List<MessageTemplate>>(uri, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplate>> GetAsync(string id,
                                                                              RequestOptions options = null,
                                                                              CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/message_templates/{id}");
            return await GetAsJsonAsync<MessageTemplate>(uri, options, cancellationToken);
        }

        /// <summary>
        /// Create a message template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplate>> CreateAsync(MessageTemplatePatchModel template,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (template is null) throw new ArgumentNullException(nameof(template));

            var uri = new Uri(BaseAddress, "/v1/message_templates");
            return await PostAsJsonAsync<MessageTemplate>(uri, template, options, cancellationToken);
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
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/message_templates/{id}");
            return await PatchAsJsonAsync<MessageTemplate>(uri, patch, options, cancellationToken);
        }

        /// <summary>
        /// Delete a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplate>> DeleteAsync(string id,
                                                                                 RequestOptions options = null,
                                                                                 CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/message_templates/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<MessageTemplate>(request, options, cancellationToken);
        }

        /// <summary>
        /// Validate a message template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<MessageTemplateValidationResponse>> ValidateAsync(MessageTemplateValidationRequest template,
                                                                                                     RequestOptions options = null,
                                                                                                     CancellationToken cancellationToken = default)
        {
            if (template is null) throw new ArgumentNullException(nameof(template));

            var uri = new Uri(BaseAddress, "/v1/message_templates/validate");
            return await PostAsJsonAsync<MessageTemplateValidationResponse>(uri, template, options, cancellationToken);
        }
    }
}
