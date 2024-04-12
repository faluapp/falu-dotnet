using Falu.Core;

namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<IdentityVerificationReport>(backChannel, options),
                                                                                                           ISupportsListing<IdentityVerificationReport, IdentityVerificationReportsListOptions>,
                                                                                                           ISupportsRetrieving<IdentityVerificationReport>
{
    /// <inheritdoc/>
    protected override string BasePath => "/v1/identity/verification_reports";

    /// <summary>List identity verification reports.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<IdentityVerificationReport>>> ListAsync(IdentityVerificationReportsListOptions? options = null,
                                                                                      RequestOptions? requestOptions = null,
                                                                                      CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List identity verification reports recursively.</summary>
    /// <inheritdoc/>
    public IAsyncEnumerable<IdentityVerificationReport> ListRecursivelyAsync(IdentityVerificationReportsListOptions? options = null,
                                                                             RequestOptions? requestOptions = null,
                                                                             CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve an identity verification report.
    /// </summary>
    /// <param name="id">Unique identifier for the identity verification report</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityVerificationReport>> GetAsync(string id,
                                                                               RequestOptions? options = null,
                                                                               CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }
}