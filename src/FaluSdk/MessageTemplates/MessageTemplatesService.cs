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
    public class MessageTemplatesService : BaseService<MessageTemplate>
    {
        ///
        public MessageTemplatesService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/message_templates";

        /// <summary>
        /// List message templates.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<MessageTemplate>>> ListAsync(BasicListOptions? options = null,
                                                                               RequestOptions? requestOptions = null,
                                                                               CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageTemplate>> GetAsync(string id,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Create a message template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageTemplate>> CreateAsync(MessageTemplatePatchModel template,
                                                                           RequestOptions? options = null,
                                                                           CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(template, options, cancellationToken);
        }

        /// <summary>
        /// Update a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<MessageTemplate>> UpdateAsync(string id,
                                                                           JsonPatchDocument<MessageTemplatePatchModel> patch,
                                                                           RequestOptions? options = null,
                                                                           CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }

        /// <summary>
        /// Delete a message template.
        /// </summary>
        /// <param name="id">Unique identifier for the message template</param>
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
