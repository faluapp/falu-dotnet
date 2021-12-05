using Falu.Core;
using Tingle.Extensions.JsonPatch;

namespace Falu.Evaluations
{
    ///
    public class EvaluationsServiceClient : BaseServiceClient<Evaluation>, ISupportsListing<Evaluation, EvaluationsListOptions>
    {
        ///
        public EvaluationsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/evaluations";

        /// <summary>List evaluations.</summary>
        /// <inheritdoc/>
        public virtual Task<ResourceResponse<List<Evaluation>>> ListAsync(EvaluationsListOptions? options = null,
                                                                          RequestOptions? requestOptions = null,
                                                                          CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>List evaluations recursively.</summary>
        /// <inheritdoc/>
        public IAsyncEnumerable<Evaluation> ListRecursivelyAsync(EvaluationsListOptions? options = null,
                                                                 RequestOptions? requestOptions = null,
                                                                 CancellationToken cancellationToken = default)
        {
            return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Evaluation>> GetAsync(string id,
                                                                   RequestOptions? options = null,
                                                                   CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
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
        public virtual Task<ResourceResponse<Evaluation>> CreateAsync(EvaluationCreateRequest evaluation,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(evaluation, options, cancellationToken);
        }

        /// <summary>
        /// Score an evaluation.
        /// </summary>
        /// <param name="id">Unique identifier for the evaluation</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Evaluation>> ScoreAsync(string id,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
        {
            var uri = $"{MakeResourcePath(id)}/score";
            return RequestAsync<Evaluation>(uri, HttpMethod.Post, new { }, options, cancellationToken);
        }
    }
}
