using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Falu.Tests
{
    public class RetryAfterPolicyTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public async Task RetryAfterPolicy_Works(int retryCount) 
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return PrepareTooManyRequestsResponseMessage();
            });

            var policy = IServiceCollectionExtensions.GetRetryAfterPolicy(retryCount);
            await TestAsync(policy, handler, (finalRetryCount) => 
            {
                Assert.Equal(retryCount, finalRetryCount);
            });
        }

        [Theory]
        [MemberData(nameof(HttpResponseData.Data), MemberType = typeof(HttpResponseData))]
        public async Task RetryAfterPolicy_Works2(HttpResponseMessage message, bool retried) 
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return message;
            });

            var retryCount = 3;
            var policy = IServiceCollectionExtensions.GetRetryAfterPolicy(retryCount);

            await TestAsync(policy, handler, (finalRetryCount) =>
            {
                var expectedRetryCount = retried ? retryCount : 0;
                Assert.Equal(expectedRetryCount, finalRetryCount);
            });
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public async Task WrappedRetryAfterPolicy_Works(int retryCount)
        {
            var handler = new DynamicHttpMessageHandler((request, ct) =>
            {
                return PrepareTooManyRequestsResponseMessage();
            });

            var retryAfterPolicy = IServiceCollectionExtensions.GetRetryAfterPolicy(retryCount);
            var generalRetryPolicy = IServiceCollectionExtensions.GetGeneralRetryPolicy(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) });
            var policy = Policy.WrapAsync(generalRetryPolicy, retryAfterPolicy);

            await TestAsync(policy, handler, (finalRetryCount) =>
            {
                Assert.Equal(retryCount, finalRetryCount);
            });
        }

        public class HttpResponseData 
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

            var retryCountKey = IServiceCollectionExtensions.RetryCount;
            var context = new Context
            {
                {retryCountKey, 0}
            };

            var response = await policy.ExecuteAsync(ctx => client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/test") { }), context);
            var finalRetryCount = (int)context[retryCountKey];
            verify(finalRetryCount);
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
