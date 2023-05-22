using Falu.Core;
using Falu.MessageStreams;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients;

public class MessageStreamsServiceClientTests : BaseServiceClientTests<MessageStream>
{
    public MessageStreamsServiceClientTests() : base(new()
    {
        Id = "mstr_123",
        Name = "default",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
    }, "/v1/message_streams")
    { }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageStreams.GetAsync(Data!.Id!, options);
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
            var opt = new MessageStreamsListOptions
            {
                Count = 1
            };

            var response = await client.MessageStreams.ListAsync(opt, options);

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
    [MemberData(nameof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions options)
    {
        var handler = ListAsync_Handler(options: options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessageStreamsListOptions
            {
                Count = 1
            };

            var results = new List<MessageStream>();
            await foreach (var item in client.MessageStreams.ListRecursivelyAsync(opt, options))
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
            var model = new MessageStreamCreateRequest
            {
                Name = Data!.Name,
                Type = Data!.Type,
            };

            var response = await client.MessageStreams.CreateAsync(model, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task ArchiveAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id}/archive", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(Data!), Encoding.UTF8, MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageStreamArchiveRequest { };
            var response = await client.MessageStreams.ArchiveAsync(Data!.Id!, model, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id!, response.Resource!.Id!);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task UnarchiveAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id!}/unarchive", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(Data!), Encoding.UTF8, MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageStreamUnarchiveRequest { };
            var response = await client.MessageStreams.UnarchiveAsync(Data!.Id!, model, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id!, response.Resource!.Id);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task UpdateAsync_Works(RequestOptions options)
    {
        var handler = UpdateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var document = new JsonPatchDocument<MessageStreamPatchModel>();
            document.Replace(x => x.Description, "new description");

            var response = await client.MessageStreams.UpdateAsync(Data!.Id!, document, options);

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
            var response = await client.MessageStreams.DeleteAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }

}
