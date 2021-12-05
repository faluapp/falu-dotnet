using Falu.Core;
using Falu.Transfers;
using System.Net;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients
{
    public class TransfersServiceClientTests : BaseServiceClientTests<Transfer>
    {
        public TransfersServiceClientTests() : base(new()
        {
            Id = "tr_123",
            Currency = "KES",
            Amount = 100_00,
            Created = DateTimeOffset.UtcNow,
            Updated = DateTimeOffset.UtcNow,
            Mpesa = new TransferMpesaDetails { },
            WorkspaceId = WorkspaceId
        }, "/v1/transfers") { }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task GetAsync_Works(RequestOptions options)
        {
            var handler = GetAsync_Handler(options);

            await TestAsync(handler, async (client) =>
            {
                var response = await client.Transfers.GetAsync(Data!.Id!, options);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
                Assert.Equal(Data!.Id, response.Resource!.Id);
            });
        }

        [Theory]
        [MemberData(nameof(RequestOptionsWithHasContinuationTokenData))]
        public async Task ListAsync_Works(RequestOptions options, bool hasContinuationToken)
        {
            var handler = ListAsync_Handler(hasContinuationToken, options);

            await TestAsync(handler, async (client) =>
            {
                var opt = new TransfersListOptions
                {
                    Count = 1
                };

                var response = await client.Transfers.ListAsync(opt, options);

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
                var opt = new TransfersListOptions
                {
                    Count = 1
                };

                var results = new List<Transfer>();

                await foreach (var item in client.Transfers.ListRecursivelyAsync(opt, options))
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
        public async Task CreateAsync_Works(RequestOptions options)
        {
            var handler = CreateAsync_Handler(options);

            await TestAsync(handler, async (client) =>
            {
                var model = new TransferCreateRequest
                {
                    Amount = Data!.Amount,
                    Description = Data!.Description,
                    Purpose = Data!.Purpose,
                    Mpesa = new TransferCreateRequestMpesa
                    {
                        Customer = new TransferCreateRequestMpesaToCustomer
                        {
                            Phone = "+254722000000",
                        }
                    }
                };

                var response = await client.Transfers.CreateAsync(model, options);

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
                var document = new JsonPatchDocument<TransferPatchModel>();
                document.Replace(x => x.Description, "new description");

                var response = await client.Transfers.UpdateAsync(Data!.Id!, document, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
            });
        }
    }
}
