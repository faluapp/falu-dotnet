using Falu.Core;
using Falu.Messages;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Text;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients;

public class MessagesServiceClientTests : BaseServiceClientTests<Message>
{
    public MessagesServiceClientTests() : base(new()
    {
        Id = "msg_123",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        To = "+254722000000",
        Body = "This is a test",
        Workspace = WorkspaceId
    }, "/v1/messages")
    { }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Messages.GetAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id!, response.Resource!.Id!);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions options, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessagesListOptions
            {
                Count = 1
            };

            var response = await client.Messages.ListAsync(opt, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource);

            if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
            else Assert.Null(response.ContinuationToken);

            var msg = response!.Resource!.Single();

            Assert.Equal(Data!.Id, msg.Id);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions options)
    {
        var handler = ListAsync_Handler(options: options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessagesListOptions
            {
                Count = 1
            };

            var results = new List<Message>();

            await foreach (var item in client.Messages.ListRecursivelyAsync(opt, options))
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
    public async Task SendAsync_Works(RequestOptions options)
    {
        var handler = CreateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageCreateRequest
            {
                To = new[] { Data!.To!, },
                Body = Data!.Body
            };
            var response = await client.Messages.SendAsync(model, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task SendBatchAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/batch", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            var content = new MessageCreateResponse
            {
                Created = Data.Created,
                Ids = new[] { Data.Id!, },
                Live = Data.Live,
                Workspace = Data.Workspace,
            };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageCreateRequest
            {
                To = new[] { Data!.To!, },
                Body = Data!.Body
            };

            var response = await client.Messages.SendBatchAsync(new[] { model }, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource!.Ids);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task UpdateAsync_Works(RequestOptions options)
    {
        var handler = UpdateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var document = new JsonPatchDocument<MessagePatchModel>();
            document.Replace(x => x.Metadata, new Dictionary<string, string> { ["purpose"] = "loan-repayment" });

            var response = await client.Messages.UpdateAsync(Data!.Id!, document, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
