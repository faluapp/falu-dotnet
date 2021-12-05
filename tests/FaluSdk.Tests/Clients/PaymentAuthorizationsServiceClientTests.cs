using Falu.Core;
using Falu.PaymentAuthorizations;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Text;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients
{
    public class PaymentAuthorizationsServiceClientTests : BaseServiceClientTests<PaymentAuthorization>
    {
        public PaymentAuthorizationsServiceClientTests() : base(new()
        {
            Id = "pauth_123",
            Currency = "KES",
            Amount = 5_000_00,
            Created = DateTimeOffset.UtcNow,
            Updated = DateTimeOffset.UtcNow,
            Mpesa = new Payments.PaymentMpesaDetails
            {
                Phone = "+254722000000",
                Reference = "MDX678TSQ",
                BusinessShortCode = "5001"
            },
            WorkspaceId = WorkspaceId
        }, "/v1/payment_authorizations") { }

        [Theory]
        [MemberData(nameof(RequestOptionsWithHasContinuationTokenData))]
        public async Task ListAsync_Works(RequestOptions options, bool hasContinuationToken)
        {
            var handler = ListAsync_Handler(hasContinuationToken, options);

            await TestAsync(handler, async (client) =>
            {
                var opt = new PaymentAuthorizationsListOptions
                {
                    Count = 1
                };

                var response = await client.PaymentAuthorizations.ListAsync(opt, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
                Assert.Single(response.Resource);

                if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
                else Assert.Null(response.ContinuationToken);

                var ev = response!.Resource!.Single();

                Assert.Equal(Data!.Id, ev.Id);
            });
        }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task ListRecursivelyAsync_Works(RequestOptions options)
        {
            var handler = ListAsync_Handler(options: options);

            await TestAsync(handler, async (client) =>
            {
                var opt = new PaymentAuthorizationsListOptions
                {
                    Count = 1
                };

                var results = new List<PaymentAuthorization>();

                await foreach (var item in client.PaymentAuthorizations.ListRecursivelyAsync(opt, options))
                {
                    results.Add(item);
                }

                Assert.Single(results);
                var ev = results.Single();
                Assert.Equal(Data!.Id, ev.Id);
            });
        }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task ApproveAsync_Works(RequestOptions options)
        {
            var handler = new DynamicHttpMessageHandler((req, ct) =>
            {
                Assert.Equal(HttpMethod.Post, req.Method);
                Assert.Equal($"{BasePath}/{Data!.Id}/approve", req.RequestUri!.AbsolutePath);

                AssertRequestHeaders(req, options);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(Data!), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                return response;
            });

            await TestAsync(handler, async (client) =>
            {
                var model = new PaymentAuthorizationApproveOptions { };
                var response = await client.PaymentAuthorizations.ApproveAsync(Data!.Id!, model, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
            });
        }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task DeclineAsync_Works(RequestOptions options)
        {
            var handler = new DynamicHttpMessageHandler((req, ct) =>
            {
                Assert.Equal(HttpMethod.Post, req.Method);
                Assert.Equal($"{BasePath}/{Data!.Id}/decline", req.RequestUri!.AbsolutePath);

                AssertRequestHeaders(req, options);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(Data!), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                return response;
            });

            await TestAsync(handler, async (client) =>
            {
                var model = new PaymentAuthorizationDeclineOptions { };
                var response = await client.PaymentAuthorizations.DeclineAsync(Data!.Id!, model, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
            });
        }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task UpdateAsync_Works(RequestOptions options)
        {
            var handler = UpdateAsync_Handler(options);

            await TestAsync(handler, async (client) =>
            {
                var document = new JsonPatchDocument<PaymentAuthorizationPatchModel>();
                document.Replace(x => x.Metadata, new Dictionary<string, string> { ["reason"] = "loan-repayment" });

                var response = await client.PaymentAuthorizations.UpdateAsync(Data!.Id!, document, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
            });
        }
    }
}
