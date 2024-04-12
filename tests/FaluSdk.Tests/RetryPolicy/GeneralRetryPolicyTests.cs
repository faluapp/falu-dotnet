using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net;
using Xunit;

namespace Falu.Tests.RetryPolicy;

public class GeneralRetryPolicyTests
{
    [Theory]
    [ClassData(typeof(HttpResponseData))]
    public void ShouldRetry_Works(HttpResponseMessage message, bool shouldRetry)
    {
        var result = IServiceCollectionExtensions.ShouldRetry(message);
        Assert.Equal(shouldRetry, result);
    }

    [Theory]
    [ClassData(typeof(HttpResponseData))]
    public async Task GeneralRetryPolicy_Works(HttpResponseMessage message, bool shouldRetry)
    {
        var executions = 0;
        var handler = new DynamicHttpMessageHandler((request, ct) =>
        {
            Interlocked.Increment(ref executions);
            return message;
        });

        var delays = new[] { TimeSpan.FromSeconds(0.5f), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1.5f) };
        var policy = IServiceCollectionExtensions.GetGeneralRetryPolicy(delays);

        await TestAsync(policy, handler);
        Assert.Equal((shouldRetry ? delays.Length : 0) + 1, executions);
    }

    [Theory]
    [ClassData(typeof(HttpResponseData))]
    public async Task WrappedGeneralRetryPolicy_Works(HttpResponseMessage message, bool shouldRetry)
    {
        var executions = 0;
        var handler = new DynamicHttpMessageHandler((request, ct) =>
        {
            Interlocked.Increment(ref executions);
            return message;
        });

        var delays = new[] { TimeSpan.FromSeconds(0.5f), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1.5f) };

        var generalRetryPolicy = IServiceCollectionExtensions.GetGeneralRetryPolicy(delays);
        var retryAfterPolicy = IServiceCollectionExtensions.GetRetryAfterPolicy(delays.Length);
        var policy = Policy.WrapAsync(generalRetryPolicy, retryAfterPolicy);

        await TestAsync(policy, handler);
        Assert.Equal((shouldRetry ? delays.Length : 0) + 1, executions);
    }

    class HttpResponseData : TheoryData<HttpResponseMessage, bool>
    {
        public HttpResponseData()
        {
            Add(PrepareShouldRetryResponseMessage(true), true);
            Add(PrepareShouldRetryResponseMessage(false), false);
            Add(new HttpResponseMessage(HttpStatusCode.BadRequest), false);
            Add(new HttpResponseMessage(HttpStatusCode.OK), false);
            Add(new HttpResponseMessage(HttpStatusCode.InternalServerError), true);
            Add(new HttpResponseMessage(HttpStatusCode.Conflict), true);
            Add(new HttpResponseMessage(HttpStatusCode.RequestTimeout), true);
            Add(new HttpResponseMessage(HttpStatusCode.GatewayTimeout), true);
            Add(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed), false);
            Add(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType), false);
            Add(new HttpResponseMessage(HttpStatusCode.Unauthorized), false);
            Add(new HttpResponseMessage(HttpStatusCode.Forbidden), false);
            Add(new HttpResponseMessage(HttpStatusCode.NoContent), false);
            Add(new HttpResponseMessage(HttpStatusCode.NotFound), false);
        }
    }

    private static HttpResponseMessage PrepareShouldRetryResponseMessage(bool shouldRetry)
    {
        var message = new HttpResponseMessage(HttpStatusCode.BadRequest);
        var headers = message.Headers;
        headers.Add(Core.HeadersNames.XShouldRetry, shouldRetry.ToString());
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
