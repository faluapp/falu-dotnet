﻿using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Evaluations
{
    ///
    public class EvaluationsService : BaseService<Evaluation>
    {
        ///
        public EvaluationsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/evaluations";

        /// <summary>
        /// List evaluations.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Evaluation>>> ListAsync(EvaluationsListOptions? options = null,
                                                                                RequestOptions? requestOptions = null,
                                                                                CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = $"/v1/evaluations{query}";
            return await GetResourceAsync<List<Evaluation>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Evaluation>> GetAsync(string id,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = $"/v1/evaluations/{id}";
            return await GetResourceAsync<Evaluation>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Evaluation>> UpdateAsync(string id,
                                                                      JsonPatchDocument<EvaluationPatchModel> patch,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }

        /// <summary>
        /// Initiate an evaluation.
        /// </summary>
        /// <param name="evaluation"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Evaluation>> CreateAsync(EvaluationCreateModel evaluation,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
        {
            if (evaluation is null) throw new ArgumentNullException(nameof(evaluation));
            if (evaluation.Scope is null) throw new InvalidOperationException($"{nameof(evaluation.Scope)} cannot be null.");
            if (evaluation.Provider is null) throw new InvalidOperationException($"{nameof(evaluation.Provider)} cannot be null.");
            if (string.IsNullOrWhiteSpace(evaluation.Name))
            {
                throw new InvalidOperationException($"{nameof(evaluation.Name)} cannot be null or whitespace.");
            }

            var content = new MultipartFormDataContent
            {
                // populate fields of the model as key value pairs
                { new StringContent(evaluation.Currency), "currency" },
                { new StringContent(evaluation.Scope?.GetEnumMemberAttrValueOrDefault()), "scope" },
                { new StringContent(evaluation.Provider?.GetEnumMemberAttrValueOrDefault()), "provider" },
                { new StringContent(evaluation.Name), "name" },

                // populate the file stream
                { new StreamContent(evaluation.Content), "file", evaluation.FileName },
            };

            // Add phone if provided
            if (!string.IsNullOrWhiteSpace(evaluation.Phone))
            {
                content.Add(new StringContent(evaluation.Phone), "phone");
            }

            // Add password if provided
            if (!string.IsNullOrWhiteSpace(evaluation.Password))
            {
                content.Add(new StringContent(evaluation.Password), "password");
            }

            // Add description if provided
            if (!string.IsNullOrWhiteSpace(evaluation.Description))
            {
                content.Add(new StringContent(evaluation.Description), "description");
            }

            // Add metadata if provided
            var metadata = evaluation.Metadata?.ToList();
            if (metadata != null)
            {
                for (var i = 0; i < metadata.Count; i++)
                {
                    content.Add(new StringContent(metadata[i].Key), $"metadata[{i}].Key");
                    content.Add(new StringContent(metadata[i].Value), $"metadata[{i}].Value");
                }
            }

            var uri = "/v1/evaluations";
            var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };
            return await SendAsync<Evaluation>(request, options, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>
        /// Score an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Evaluation>> ScoreAsync(string id,
                                                                           RequestOptions? options = null,
                                                                           CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = $"/v1/evaluations/{id}/score";
            return await PostAsync<Evaluation>(uri, new { }, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
