using Falu.Core;
using System.Net.Http.Json;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Payments;

///
public class MoneyBalancesServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<MoneyBalances>(backChannel, options)
{
    /// <inheritdoc/>
    protected override string BasePath => "/v1/money_balances";

    /// <summary>
    /// Retrieve balance.
    /// </summary>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MoneyBalances>> GetAsync(RequestOptions? options = null,
                                                                  CancellationToken cancellationToken = default)
    {
        var uri = MakePath();
        return RequestResourceAsync(uri, HttpMethod.Get, null, options, cancellationToken);
    }

    /// <summary>
    /// Force a balance refresh.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MoneyBalancesRefreshResponse>> RefreshAsync(MoneyBalancesRefreshRequest request,
                                                                                     RequestOptions? options = null,
                                                                                     CancellationToken cancellationToken = default)
    {
        var uri = MakePath("/refresh");
        var content = JsonContent.Create(request, SC.Default.MoneyBalancesRefreshRequest);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.MoneyBalancesRefreshResponse, content, options, cancellationToken);
    }
}
