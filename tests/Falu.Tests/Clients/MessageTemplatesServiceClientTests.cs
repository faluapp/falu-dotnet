using Falu.Core;
using Falu.MessageTemplates;
using Falu.Serialization;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Falu.Tests.Clients;

public class MessageTemplatesServiceClientTests : BaseServiceClientTests<MessageTemplate>
{
    public MessageTemplatesServiceClientTests() : base(new()
    {
        Id = "mtpl_123",
        Alias = "loyalty",
        Body = "Hi {{name}}! Thanks for being a loyal customer. We appreciate you!",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
    }, "/v1/message_templates")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageTemplates.GetAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions requestOptions, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessageTemplatesListOptions
            {
                Count = 1
            };

            var response = await client.MessageTemplates.ListAsync(opt, requestOptions);

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
    [ClassData(typeof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions requestOptions)
    {
        var handler = ListAsync_Handler(requestOptions: requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessageTemplatesListOptions
            {
                Count = 1
            };

            var results = new List<MessageTemplate>();

            await foreach (var item in client.MessageTemplates.ListRecursivelyAsync(opt, requestOptions))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var mtpl = results.Single();
            Assert.Equal(Data!.Id, mtpl.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions requestOptions)
    {
        var handler = CreateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageTemplateCreateOptions
            {
                Alias = Data!.Alias,
                Body = Data!.Body
            };

            var response = await client.MessageTemplates.CreateAsync(model, requestOptions);

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
            var options = new MessageTemplateUpdateOptions
            {
                Description = "new description"
            };
            var response = await client.MessageTemplates.UpdateAsync(Data!.Id!, options, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task DeleteAsync_Works(RequestOptions requestOptions)
    {
        var handler = DeleteAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageTemplates.DeleteAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task ValidateAsync_Works(RequestOptions requestOptions)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal(ApiHost, req.RequestUri!.Host);
            Assert.Equal($"{BasePath}/validate", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, requestOptions);

            var content = new MessageTemplateValidationResponse
            {
                Rendered = "Hi cake! Thanks for being a loyal customer. We appreciate you!",
                Model = MessageTemplateModel.Create(
                    new Dictionary<string, string> { ["name"] = "cake" },
                    FaluSerializerContext.Default.DictionaryStringString),
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(content, FaluSerializerContext.Default.MessageTemplateValidationResponse),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageTemplateValidationOptions
            {
                Model = MessageTemplateModel.Create(
                    new Dictionary<string, string> { ["name"] = "cake" },
                    FaluSerializerContext.Default.DictionaryStringString),
                Body = Data!.Body
            };

            var response = await client.MessageTemplates.ValidateAsync(model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
