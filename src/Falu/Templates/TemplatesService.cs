using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Templates
{
    ///
    public class TemplatesService : BaseService
    {
        ///
        public TemplatesService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List templates.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Template>>> ListAsync(BasicListOptions options = null,
                                                                              CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/templates{query}");
            return await GetAsJsonAsync<List<Template>>(uri, cancellationToken);
        }

        /// <summary>
        /// Retrieve a template.
        /// </summary>
        /// <param name="id">Unique identifier for the template</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Template>> GetAsync(string id,
                                                                       CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/templates/{id}");
            return await GetAsJsonAsync<Template>(uri, cancellationToken);
        }

        /// <summary>
        /// Create a template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Template>> CreateAsync(TemplatePatchModel template,
                                                                          CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/templates");
            return await PostAsJsonAsync<Template>(uri, template, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update a template.
        /// </summary>
        /// <param name="id">Unique identifier for the template</param>
        /// <param name="patch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Template>> UpdateAsync(string id,
                                                                          JsonPatchDocument<TemplatePatchModel> patch,
                                                                          CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/templates/{id}");
            return await PatchAsJsonAsync<Template>(uri, patch, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Delete a template.
        /// </summary>
        /// <param name="id">Unique identifier for the template</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Template>> DeleteAsync(string id,
                                                                          CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/templates/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<Template>(request, cancellationToken);
        }

        /// <summary>
        /// Validate a template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<TemplateValidationResponse>> ValidateAsync(TemplateValidationRequest template,
                                                                                              CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/templates/validate");
            return await PostAsJsonAsync<TemplateValidationResponse>(uri, template, cancellationToken: cancellationToken);
        }
    }
}
