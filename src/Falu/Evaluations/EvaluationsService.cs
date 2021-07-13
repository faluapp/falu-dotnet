using Falu.Core;
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
    public class EvaluationsService : BaseService
    {
        ///
        public EvaluationsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

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
            var uri = new Uri(BaseAddress, $"/v1/evaluations{query}");
            return await GetAsJsonAsync<List<Evaluation>>(uri, requestOptions, cancellationToken);
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

            var uri = new Uri(BaseAddress, $"/v1/evaluations/{id}");
            return await GetAsJsonAsync<Evaluation>(uri, options, cancellationToken);
        }

        /// <summary>
        /// Update an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Evaluation>> UpdateAsync(string id,
                                                                            JsonPatchDocument<EvaluationPatchModel> patch,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/evaluations/{id}");
            return await PatchAsJsonAsync<Evaluation>(uri, patch, options, cancellationToken);
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

            var content = new MultipartFormDataContent
            {
                // populate fields of the model as key value pairs
                { new StringContent(evaluation.Currency), nameof(evaluation.Currency) },
                { new StringContent(evaluation.Scope.ToString()), nameof(evaluation.Scope) },
                { new StringContent(evaluation.Provider.ToString()), nameof(evaluation.Provider) },
                { new StringContent(evaluation.Name), nameof(evaluation.Name) },
                { new StringContent(evaluation.Phone), nameof(evaluation.Phone) },
                { new StringContent(evaluation.Password), nameof(evaluation.Password) },

                // populate the file stream
                { new StreamContent(evaluation.Content), "File", evaluation.FileName },
            };

            // Add description if provided
            if (!string.IsNullOrWhiteSpace(evaluation.Description))
            {
                content.Add(new StringContent(evaluation.Description), nameof(evaluation.Description));
            }

            // Add tags if provided
            var tags = evaluation.Tags;
            if (tags != null)
            {
                for (var i = 0; i < tags.Count; i++)
                {
                    content.Add(new StringContent(tags[i]), $"{nameof(evaluation.Tags)}[{i}]");
                }
            }

            // Add metadata if provided
            var metadata = evaluation.Metadata?.ToList();
            if (metadata != null)
            {
                for (var i = 0; i < metadata.Count; i++)
                {
                    content.Add(new StringContent(metadata[i].Key), $"{nameof(evaluation.Metadata)}[{i}].Key");
                    content.Add(new StringContent(metadata[i].Value), $"{nameof(evaluation.Metadata)}[{i}].Value");
                }
            }

            var uri = new Uri(BaseAddress, "/v1/evaluations");
            var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };
            return await SendAsync<Evaluation>(request, options, cancellationToken);
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

            var uri = new Uri(BaseAddress, $"/v1/evaluations/{id}/score");
            return await PostAsJsonAsync<Evaluation>(uri, new { }, options, cancellationToken);
        }
    }
}
