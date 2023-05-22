using Falu.Core;

namespace Falu.Payments;

///
public class MoneyBalancesServiceClient : BaseServiceClient<MoneyBalances>
{
    ///
    public MoneyBalancesServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

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
        return RequestAsync<MoneyBalances>(uri, HttpMethod.Get, null, options, cancellationToken);
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
        return RequestAsync<MoneyBalancesRefreshResponse>(uri, HttpMethod.Post, request, options, cancellationToken);
    }
}
