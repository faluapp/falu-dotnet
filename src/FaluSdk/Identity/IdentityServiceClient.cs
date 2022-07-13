using Falu.Core;

namespace Falu.Identity;

///
[Obsolete(MessageStrings.IdentitySearchDeprecated)]
public class IdentityServiceClient : BaseServiceClient<IdentityRecord>
{
    ///
    public IdentityServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/identity";

    /// <summary>
    /// Search for an entity's identity.
    /// </summary>
    /// <param name="search">The details to use for searching.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<IdentityRecord>> SearchAsync(IdentitySearchModel search,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var uri = MakePath("/search");
        return RequestAsync<IdentityRecord>(uri, HttpMethod.Post, search, options, cancellationToken);
    }
}
