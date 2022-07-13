using Falu.Core;
using Falu.MessageTemplates;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients;

public class MessageTemplatesServiceClientTests : BaseServiceClientTests<MessageTemplate>
{
    public MessageTemplatesServiceClientTests() : base(new()
    {
        Id = "tmpl_123",
        Alias = "loyalty",
        Body = "Hi {{name}}! Thanks for being a loyal customer. We appreciate you!",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
    }, "/v1/message_templates")
    { }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageTemplates.GetAsync(Data!.Id!, options);
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
            var opt = new MessageTemplatesListOptions
            {
                Count = 1
            };

            var response = await client.MessageTemplates.ListAsync(opt, options);

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
            var opt = new MessageTemplatesListOptions
            {
                Count = 1
            };

            var results = new List<MessageTemplate>();

            await foreach (var item in client.MessageTemplates.ListRecursivelyAsync(opt, options))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var msgtmpl = results.Single();
            Assert.Equal(Data!.Id, msgtmpl.Id);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions options)
    {
        var handler = CreateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageTemplateCreateRequest
            {
                Alias = Data!.Alias,
                Body = Data!.Body
            };

            var response = await client.MessageTemplates.CreateAsync(model, options);

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
            var document = new JsonPatchDocument<MessageTemplatePatchModel>();
            document.Replace(x => x.Description, "new description");

            var response = await client.MessageTemplates.UpdateAsync(Data!.Id!, document, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task DeleteAsync_Works(RequestOptions options)
    {
        var handler = DeleteAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageTemplates.DeleteAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task ValidateAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/validate", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            var content = new MessageTemplateValidationResponse
            {
                Rendered = "Hi cake! Thanks for being a loyal customer. We appreciate you!"
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageTemplateValidationRequest
            {
                Model = new Dictionary<string, string> { ["name"] = "cake" },
                Body = Data!.Body
            };

            var response = await client.MessageTemplates.ValidateAsync(model, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
