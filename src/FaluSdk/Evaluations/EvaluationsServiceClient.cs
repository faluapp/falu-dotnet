using Falu.Core;
using Tingle.Extensions.JsonPatch;

namespace Falu.Evaluations;

///
public class EvaluationsServiceClient : BaseServiceClient<Evaluation>,
                                        ISupportsListing<Evaluation, EvaluationsListOptions>,
                                        ISupportsRetrieving<Evaluation>,
                                        ISupportsCreation<Evaluation, EvaluationCreateRequest>,
                                        ISupportsUpdating<Evaluation, EvaluationPatchModel>,
                                        ISupportsCanceling<Evaluation>,
                                        ISupportsRedaction<Evaluation>
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
    /// <param name="id">Unique identifier for the evaluation.</param>
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
    /// <param name="id">Unique identifier for the evaluation.</param>
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
    /// Create an evaluation.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Evaluation>> CreateAsync(EvaluationCreateRequest request,
                                                                  RequestOptions? options = null,
                                                                  CancellationToken cancellationToken = default)
    {
        return CreateResourceAsync(request, options, cancellationToken);
    }

    /// <summary>Cancel an evaluation preventing further updates.</summary>
    /// <param name="id">Unique identifier for the evaluation.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<Evaluation>> CancelAsync(string id,
                                                          RequestOptions? options = null,
                                                          CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/cancel";
        return RequestAsync<Evaluation>(uri, HttpMethod.Post, new { }, options, cancellationToken);
    }

    /// <summary>Redact an evaluation to remove all collected information from Falu.</summary>
    /// <param name="id">Unique identifier for the evaluation.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<Evaluation>> RedactAsync(string id,
                                                          RequestOptions? options = null,
                                                          CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/redact";
        return RequestAsync<Evaluation>(uri, HttpMethod.Post, new { }, options, cancellationToken);
    }
}
