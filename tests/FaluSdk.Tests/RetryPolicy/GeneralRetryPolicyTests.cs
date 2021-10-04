using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Falu.Tests.RetryPolicy
{
    public class GeneralRetryPolicyTests
    {
        [Theory]
        [MemberData(nameof(HttpResponseData.Data), MemberType = typeof(HttpResponseData))]
        public void ShouldRetry_Works(HttpResponseMessage message, bool shouldRetry)
        {
            var result = IServiceCollectionExtensions.ShouldRetry(message);
            Assert.Equal(shouldRetry, result);
        }

        [Theory]
        [MemberData(nameof(HttpResponseData.Data), MemberType = typeof(HttpResponseData))]
        public async Task GeneralRetryPolicy_Works(HttpResponseMessage message, bool shouldRetry) 
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return message;
            });

            var delays = new[] { TimeSpan.FromSeconds(0.5f), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1.5f) };
            var policy = IServiceCollectionExtensions.GetGeneralRetryPolicy(delays);

            await TestAsync(policy, handler, (attempts) =>
            {
                var retries = shouldRetry ? delays.Length : 0;
                Assert.Equal(retries, attempts);
            });
        }

        [Theory]
        [MemberData(nameof(HttpResponseData.Data), MemberType = typeof(HttpResponseData))]
        public async Task WrappedGeneralRetryPolicy_Works(HttpResponseMessage message, bool shouldRetry)
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return message;
            });

            var delays = new[] { TimeSpan.FromSeconds(0.5f), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1.5f) };

            var generalRetryPolicy = IServiceCollectionExtensions.GetGeneralRetryPolicy(delays);
            var retryAfterPolicy = IServiceCollectionExtensions.GetRetryAfterPolicy(delays.Length);
            var policy = Policy.WrapAsync(generalRetryPolicy, retryAfterPolicy);

            await TestAsync(policy, handler, (attempts) =>
            {
                var retries = shouldRetry ? delays.Length : 0;
                Assert.Equal(retries, attempts);
            });
        }

        class HttpResponseData
        {
            public static IEnumerable<object[]> Data => new List<object[]>
            {
                new object[] { PrepareShouldRetryResponseMessage(true), true },
                new object[] { PrepareShouldRetryResponseMessage(false), false },
                new object[] { new HttpResponseMessage(HttpStatusCode.BadRequest), false },
                new object[] { new HttpResponseMessage(HttpStatusCode.OK), false},
                new object[] { new HttpResponseMessage(HttpStatusCode.InternalServerError), true },
                new object[] { new HttpResponseMessage(HttpStatusCode.Conflict), true},
                new object[] { new HttpResponseMessage(HttpStatusCode.RequestTimeout), true},
                new object[] { new HttpResponseMessage(HttpStatusCode.GatewayTimeout), true},
                new object[] { new HttpResponseMessage(HttpStatusCode.MethodNotAllowed), false},
                new object[] { new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType), false},
                new object[] { new HttpResponseMessage(HttpStatusCode.Unauthorized), false},
                new object[] { new HttpResponseMessage(HttpStatusCode.NoContent), false},
                new object[] { new HttpResponseMessage(HttpStatusCode.NotFound), false}
            };
        }

        private static HttpResponseMessage PrepareShouldRetryResponseMessage(bool shouldRetry)
        {
            var message = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var headers = message.Headers;
            headers.Add(Core.HeadersNames.XShouldRetry, shouldRetry.ToString());
            return message;
        }

        private static async Task TestAsync(AsyncPolicy<HttpResponseMessage> policy, HttpMessageHandler handler, Action<int> verify)
        {
            var services = new ServiceCollection();
            services.AddHttpClient(nameof(FaluClient))
                    .ConfigureHttpClient((serviceProvider, client) =>
                    {
                        client.BaseAddress = new Uri("https://api-test.falu.io/");
                    })
                    .ConfigurePrimaryHttpMessageHandler(() => handler)
                    .AddPolicyHandler(policy);

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var sp = scope.ServiceProvider;
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient(nameof(FaluClient));

            var attemptsKey = IServiceCollectionExtensions.Attempts;
            var context = new Context
            {
                {attemptsKey, 0}
            };

            var response = await policy.ExecuteAsync(ctx => client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/test") { }), context);
            var attempts = (int)context[attemptsKey];
            verify(attempts);
        }
    }
}
