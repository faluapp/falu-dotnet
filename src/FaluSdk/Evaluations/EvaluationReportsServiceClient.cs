using Falu.Core;

namespace Falu.Evaluations;

///
public class EvaluationReportsServiceClient : BaseServiceClient<EvaluationReport>,
                                              ISupportsListing<EvaluationReport, EvaluationReportsListOptions>,
                                              ISupportsRetrieving<EvaluationReport>
{
    ///
    public EvaluationReportsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/evaluations/evaluation_reports";

    /// <summary>List evaluation reports.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<EvaluationReport>>> ListAsync(EvaluationReportsListOptions? options = null,
                                                                            RequestOptions? requestOptions = null,
                                                                            CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List evaluation reports recursively.</summary>
    /// <inheritdoc/>
    public IAsyncEnumerable<EvaluationReport> ListRecursivelyAsync(EvaluationReportsListOptions? options = null,
                                                                   RequestOptions? requestOptions = null,
                                                                   CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve an evaluation report.
    /// </summary>
    /// <param name="id">Unique identifier for the evaluation report</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<EvaluationReport>> GetAsync(string id,
                                                                     RequestOptions? options = null,
                                                                     CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

}