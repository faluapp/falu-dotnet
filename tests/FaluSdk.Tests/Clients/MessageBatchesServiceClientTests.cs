using Falu.Core;
using Falu.MessageBatches;
using System.Net;
using System.Net.Mime;
using System.Text;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients;

public class MessageBatchesServiceClientTests : BaseServiceClientTests<MessageBatch>
{
    public MessageBatchesServiceClientTests() : base(new()
    {
        Id = "msba_123",
        Messages = ["msg_123",],
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
    }, "/v1/message_batches")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageBatches.GetAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions options, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessageBatchesListOptions
            {
                Count = 1
            };

            var response = await client.MessageBatches.ListAsync(opt, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource);

            if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
            else Assert.Null(response.ContinuationToken);

            var msgstr = response!.Resource!.Single();
            Assert.Equal(Data!.Id!, msgstr.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions options)
    {
        var handler = ListAsync_Handler(options: options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessageBatchesListOptions
            {
                Count = 1
            };

            var results = new List<MessageBatch>();
            await foreach (var item in client.MessageBatches.ListRecursivelyAsync(opt, options))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var ev = results.Single();
            Assert.Equal(Data!.Id, ev.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions options)
    {
        var handler = CreateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageBatchCreateOptions
            {
                Messages =
                [
                    new MessageBatchCreateOptionsMessage
                    {
                        Tos = ["+254722000000"],
                        Body = "This is a test",
                    },
                ],
            };
            var response = await client.MessageBatches.CreateAsync(model, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task UpdateAsync_Works(RequestOptions options)
    {
        var handler = UpdateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var document = new JsonPatchDocument<MessageBatchUpdateOptions>();
            document.Replace(x => x.Metadata, new Dictionary<string, string> { ["purpose"] = "loan-repayment" });

            var response = await client.MessageBatches.UpdateAsync(Data!.Id!, document, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task StatusAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Get, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id}/status", req.RequestUri!.AbsolutePath);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageBatches.StatusAsync(Data!.Id!, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CancelAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id}/cancel", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageBatches.CancelAsync(Data!.Id!, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task RedactAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id}/redact", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageBatches.RedactAsync(Data!.Id!, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
