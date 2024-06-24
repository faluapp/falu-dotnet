using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.IdentityVerifications;

///
public class IdentityVerificationsServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<IdentityVerification>(backChannel, options),
                                                                                                     ISupportsListing<IdentityVerification, IdentityVerificationsListOptions>,
                                                                                                     ISupportsRetrieving<IdentityVerification>,
                                                                                                     ISupportsCreation<IdentityVerification, IdentityVerificationCreateOptions>,
                                                                                                     ISupportsUpdating<IdentityVerification, IdentityVerificationUpdateOptions>,
                                                                                                     ISupportsCanceling<IdentityVerification>,
                                                                                                     ISupportsRedaction<IdentityVerification>
{
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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityVerification>> GetAsync(string id,
                                                                         RequestOptions? requestOptions = null,
                                                                         CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Update an identity verification.
    /// </summary>
    /// <param name="id">Unique identifier for the identity verification.</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityVerification>> UpdateAsync(string id,
                                                                            JsonPatchDocument<IdentityVerificationUpdateOptions> options,
                                                                            RequestOptions? requestOptions = null,
                                                                            CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.JsonPatchDocumentIdentityVerificationUpdateOptions);
        return UpdateResourceAsync(id, content, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Create an identity verification.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityVerification>> CreateAsync(IdentityVerificationCreateOptions options,
                                                                            RequestOptions? requestOptions = null,
                                                                            CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(options, SC.Default.IdentityVerificationCreateOptions);
        return CreateResourceAsync(content, requestOptions, cancellationToken);
    }

    /// <summary>Cancel an identity verification preventing further updates.</summary>
    /// <param name="id">Unique identifier for the identity verification.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<IdentityVerification>> CancelAsync(string id,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        return CancelResourceAsync(id, null, requestOptions, cancellationToken);
    }

    /// <summary>Redact an identity verification to remove all collected information from Falu.</summary>
    /// <param name="id">Unique identifier for the identity verification.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ResourceResponse<IdentityVerification>> RedactAsync(string id,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        return RedactResourceAsync(id, null, requestOptions, cancellationToken);
    }
}
