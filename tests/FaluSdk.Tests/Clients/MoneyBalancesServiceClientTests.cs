﻿using Falu.Core;
using Falu.Payments;
using Falu.Serialization;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Falu.Tests.Clients;

public class MoneyBalancesServiceClientTests : BaseServiceClientTests
{
    private const string BasePath = "/v1/money_balances";
    private readonly MoneyBalances data = new()
    {
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        Mpesa = new Dictionary<string, long>
        {
            ["0001"] = 50_000_00
        },
    };


    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Get, req.Method);
            Assert.Equal($"{BasePath}", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(data, FaluSerializerContext.Default.MoneyBalances),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MoneyBalances.GetAsync(options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task RefreshAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/refresh", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(data, FaluSerializerContext.Default.MoneyBalances),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MoneyBalancesRefreshRequest { };
            var response = await client.MoneyBalances.RefreshAsync(model, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
