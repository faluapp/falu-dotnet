using Falu.Infrastructure;
using Falu.Messages;
using System;
using System.Collections.Generic;
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Evaluation>>> ListAsync(EvaluationsListOptions options = null,
                                                                                CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/evaluations{query}");
            return await GetAsJsonAsync<List<Evaluation>>(uri, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Retrieve an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Evaluation>> GetAsync(string id,
                                                                         CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/evaluations/{id}");
            return await GetAsJsonAsync<Evaluation>(uri, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="patch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Evaluation>> UpdateAsync(string id,
                                                                            JsonPatchDocument<EvaluationPatchModel> patch,
                                                                            CancellationToken cancellationToken = default)
        {
            var uri = new Uri(BaseAddress, $"/v1/evaluations/{id}");
            return await PatchAsJsonAsync<Evaluation>(uri, patch, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Initiate an evaluation.
        /// </summary>
        /// <param name="evaluation"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Evaluation>> CreateAsync(EvaluationRequest evaluation,
                                                                            CancellationToken cancellationToken = default)
        {
            var content = new MultipartFormDataContent
            {
                // populate fields of the model as key value pairs
                { new StringContent(evaluation.Currency), nameof(evaluation.Currency) },
                { new StringContent(evaluation.Scope.ToString()), nameof(evaluation.Scope) },
                { new StringContent(evaluation.Provider.ToString()), nameof(evaluation.Provider) },
                { new StringContent(evaluation.Name), nameof(evaluation.Name) },
                { new StringContent(evaluation.Phone), nameof(evaluation.Phone) },
                { new StringContent(evaluation.Password), nameof(evaluation.Password) },
                { new StringContent(evaluation.Description), nameof(evaluation.Description) },
                //{ new StringContent(evaluation.Metadata), nameof(evaluation.Metadata) },
                //{ new StringContent(evaluation.Tags), nameof(evaluation.Tags) },

                // populate the file stream
                { new StreamContent(evaluation.Content), "File", evaluation.FileName },
            };
            var uri = new Uri(BaseAddress, "/v1/statements/extract/mpesa");
            var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };
            return await SendAsync<Evaluation>(request, cancellationToken: cancellationToken);
        }
    }
}
