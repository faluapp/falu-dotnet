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
    public class RetryAfterPolicyTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public async Task RetryAfterPolicy_Works(int retries) 
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return PrepareTooManyRequestsResponseMessage();
            });

            var policy = IServiceCollectionExtensions.GetRetryAfterPolicy(retries);
            await TestAsync(policy, handler, (attempts) => 
            {
                Assert.Equal(retries, attempts);
            });
        }

        [Theory]
        [MemberData(nameof(HttpResponseData.Data), MemberType = typeof(HttpResponseData))]
        public async Task RetryAfterPolicy_Works2(HttpResponseMessage message, bool shouldRetry) 
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return message;
            });

            var retries = 3;
            var policy = IServiceCollectionExtensions.GetRetryAfterPolicy(retries);

            await TestAsync(policy, handler, (attempts) =>
            {
                var expectedRetries = shouldRetry ? retries : 0;
                Assert.Equal(expectedRetries, attempts);
            });
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public async Task WrappedRetryAfterPolicy_Works(int retries)
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return PrepareTooManyRequestsResponseMessage();
            });

            var retryAfterPolicy = IServiceCollectionExtensions.GetRetryAfterPolicy(retries);
            var delays = new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) };
            var generalRetryPolicy = IServiceCollectionExtensions.GetGeneralRetryPolicy(delays);
            var policy = Policy.WrapAsync(generalRetryPolicy, retryAfterPolicy);

            await TestAsync(policy, handler, (attempts) =>
            {
                Assert.Equal(retries, attempts);
            });
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

        private static HttpResponseMessage PrepareTooManyRequestsResponseMessage() 
        {
            var message = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
            var headers = message.Headers;
            var date = DateTimeOffset.UtcNow.AddSeconds(1);
            headers.Add(Core.HeadersNames.RetryAfter, date.ToString("R"));
            return message;
        }
    }
}
