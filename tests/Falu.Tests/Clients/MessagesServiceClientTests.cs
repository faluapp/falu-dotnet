using Falu.Core;
using Falu.Messages;
using Falu.Serialization;
using System.Net;
using System.Net.Mime;
using System.Text;
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
    }, "/v1/messages")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Messages.GetAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id!, response.Resource!.Id!);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions requestOptions, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessagesListOptions
            {
                Count = 1
            };

            var response = await client.Messages.ListAsync(opt, requestOptions);

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
    [ClassData(typeof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions requestOptions)
    {
        var handler = ListAsync_Handler(requestOptions: requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessagesListOptions
            {
                Count = 1
            };

            var results = new List<Message>();

            await foreach (var item in client.Messages.ListRecursivelyAsync(opt, requestOptions))
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
    public async Task CreateAsync_Works(RequestOptions requestOptions)
    {
        var handler = CreateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageCreateOptions
            {
                To = Data!.To!,
                Body = Data!.Body
            };
            var response = await client.Messages.CreateAsync(model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CreateAsync_WithTemplate_Works(RequestOptions requestOptions)
    {
        var handler = CreateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageCreateOptions
            {
                To = Data!.To!,
                Template = new MessageCreateOptionsTemplate
                {
                    Alias = "cars_list",
                    Model = MessageTemplates.MessageTemplateModel.Create(
                        new Dictionary<string, string[]> { ["registrations"] = ["123"], },
                        FaluSerializerContext.Default.IDictionaryStringStringArray),
                },
            };
            var response = await client.Messages.CreateAsync(model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task UpdateAsync_Works(RequestOptions requestOptions)
    {
        var handler = UpdateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var options = new MessageUpdateOptions
            {
                Metadata = new Dictionary<string, string> { ["purpose"] = "loan-repayment" }
            };
            var response = await client.Messages.UpdateAsync(Data!.Id!, options, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CancelAsync_Works(RequestOptions requestOptions)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal(ApiHost, req.RequestUri!.Host);
            Assert.Equal($"{BasePath}/{Data!.Id}/cancel", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, requestOptions);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Messages.CancelAsync(Data!.Id!, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task RedactAsync_Works(RequestOptions requestOptions)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal(ApiHost, req.RequestUri!.Host);
            Assert.Equal($"{BasePath}/{Data!.Id}/redact", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, requestOptions);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Messages.RedactAsync(Data!.Id!, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
