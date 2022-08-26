using Falu.Core;
using Tingle.Extensions.JsonPatch;

namespace Falu.IdentityVerifications;

///
public class IdentityVerificationsServiceClient : BaseServiceClient<IdentityVerification>,
                                                  ISupportsListing<IdentityVerification, IdentityVerificationsListOptions>,
                                                  ISupportsRetrieving<IdentityVerification>,
                                                  ISupportsCreation<IdentityVerification, IdentityVerificationCreateRequest>,
                                                  ISupportsUpdating<IdentityVerification, IdentityVerificationPatchModel>,
                                                  ISupportsCanceling<IdentityVerification>,
                                                  ISupportsRedaction<IdentityVerification>
{
    ///
    public IdentityVerificationsServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/identity/verifications";

    /// <summary>List identity verifications.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<IdentityVerification>>> ListAsync(IdentityVerificationsListOptions? options = null,
                                                                                RequestOptions? requestOptions = null,
                                                                                CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List identity verifications recursively.</summary>
    /// <inheritdoc/>
    public IAsyncEnumerable<IdentityVerification> ListRecursivelyAsync(IdentityVerificationsListOptions? options = null,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve an identity verification.
    /// </summary>
    /// <param name="id">Unique identifier for the identity verification.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityVerification>> GetAsync(string id,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Update an identity verification.
    /// </summary>
    /// <param name="id">Unique identifier for the identity verification.</param>
    /// <param name="patch"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityVerification>> UpdateAsync(string id,
                                                                            JsonPatchDocument<IdentityVerificationPatchModel> patch,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
    {
        return UpdateResourceAsync(id, patch, options, cancellationToken);
    }

    /// <summary>
    /// Create an identity verification.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityVerification>> CreateAsync(IdentityVerificationCreateRequest request,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
    {
        return CreateResourceAsync(request, options, cancellationToken);
    }

    /// <summary>Cancel an identity verification preventing further updates.</summary>
    /// <param name="id">Unique identifier for the identity verification.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<IdentityVerification>> CancelAsync(string id,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/cancel";
        return RequestAsync<IdentityVerification>(uri, HttpMethod.Post, new { }, options, cancellationToken);
    }

    /// <summary>Redact an identity verification to remove all collected information from Falu.</summary>
    /// <param name="id">Unique identifier for the identity verification.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<IdentityVerification>> RedactAsync(string id,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var uri = $"{MakeResourcePath(id)}/redact";
        return RequestAsync<IdentityVerification>(uri, HttpMethod.Post, new { }, options, cancellationToken);
    }
}
