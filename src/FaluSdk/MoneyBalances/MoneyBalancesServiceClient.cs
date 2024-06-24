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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MoneyBalances>> GetAsync(RequestOptions? requestOptions = null,
                                                                  CancellationToken cancellationToken = default)
    {
        var uri = MakePath();
        return RequestResourceAsync(uri, HttpMethod.Get, null, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Force a balance refresh.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<MoneyBalancesRefreshResponse>> RefreshAsync(MoneyBalancesRefreshOptions options,
                                                                                     RequestOptions? requestOptions = null,
                                                                                     CancellationToken cancellationToken = default)
    {
        var uri = MakePath("/refresh");
        var content = JsonContent.Create(options, SC.Default.MoneyBalancesRefreshOptions);
        return RequestAsync(uri, HttpMethod.Post, SC.Default.MoneyBalancesRefreshResponse, content, requestOptions, cancellationToken);
    }
}
