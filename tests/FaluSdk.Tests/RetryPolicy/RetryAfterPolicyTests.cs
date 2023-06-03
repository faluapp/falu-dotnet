using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net;
using Xunit;

namespace Falu.Tests.RetryPolicy;

public class RetryAfterPolicyTests
{
    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    public async Task RetryAfterPolicy_Works(int retries)
    {
        var executions = 0;
        var handler = new DynamicHttpMessageHandler((request, ct) =>
        {
            Interlocked.Increment(ref executions);
            return PrepareTooManyRequestsResponseMessage();
        });

        var policy = IServiceCollectionExtensions.GetRetryAfterPolicy(retries);
        await TestAsync(policy, handler);
        Assert.Equal(retries + 1, executions);
    }

    [Theory]
    [MemberData(nameof(HttpResponseData.Data), MemberType = typeof(HttpResponseData))]
    public async Task RetryAfterPolicy_Works2(HttpResponseMessage message, bool shouldRetry)
    {
        var executions = 0;
        var handler = new DynamicHttpMessageHandler((request, ct) =>
        {
            Interlocked.Increment(ref executions);
            return message;
        });

        var retries = 3;
        var policy = IServiceCollectionExtensions.GetRetryAfterPolicy(retries);
        await TestAsync(policy, handler);

        Assert.Equal((shouldRetry ? retries : 0) + 1, executions);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    public async Task WrappedRetryAfterPolicy_Works(int retries)
    {
        var executions = 0;
        var handler = new DynamicHttpMessageHandler((request, ct) =>
        {
            Interlocked.Increment(ref executions);
            return PrepareTooManyRequestsResponseMessage();
        });

        var retryAfterPolicy = IServiceCollectionExtensions.GetRetryAfterPolicy(retries);
        var delays = new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) };
        var generalRetryPolicy = IServiceCollectionExtensions.GetGeneralRetryPolicy(delays);
        var policy = Policy.WrapAsync(generalRetryPolicy, retryAfterPolicy);

        await TestAsync(policy, handler);
        Assert.Equal(retries + 1, executions);
    }

    class HttpResponseData
    {
        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { PrepareTooManyRequestsResponseMessage(), true },
            new object[] { new HttpResponseMessage(HttpStatusCode.BadRequest), false },
            new object[] { new HttpResponseMessage(HttpStatusCode.OK), false},
            new object[] { new HttpResponseMessage(HttpStatusCode.InternalServerError), false}
        };
    }

    private static HttpResponseMessage PrepareTooManyRequestsResponseMessage()
    {
        var message = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
        var headers = message.Headers;
        var date = DateTimeOffset.UtcNow.AddSeconds(1);
        headers.Add(Core.HeadersNames.RetryAfter, date.ToString("R"));
        return message;
    }

    private static async Task TestAsync(AsyncPolicy<HttpResponseMessage> policy, HttpMessageHandler handler)
    {
        var services = new ServiceCollection();
        services.AddHttpClient(nameof(FaluClient))
                .ConfigurePrimaryHttpMessageHandler(() => handler)
                .AddPolicyHandler(policy);

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var sp = scope.ServiceProvider;
        var factory = sp.GetRequiredService<IHttpClientFactory>();
        var client = factory.CreateClient(nameof(FaluClient));

        var request = new HttpRequestMessage(HttpMethod.Get, "https://api-test.falu.io/test");
        var response = await client.SendAsync(request);
    }
}
