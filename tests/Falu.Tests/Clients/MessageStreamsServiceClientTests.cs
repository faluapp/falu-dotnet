using Falu.Core;
using Falu.MessageStreams;
using Falu.Serialization;
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
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageStreams.GetAsync(Data!.Id!, requestOptions);
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
            var opt = new MessageStreamsListOptions
            {
                Count = 1
            };

            var response = await client.MessageStreams.ListAsync(opt, requestOptions);

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
    public async Task ListRecursivelyAsync_Works(RequestOptions requestOptions)
    {
        var handler = ListAsync_Handler(requestOptions: requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new MessageStreamsListOptions
            {
                Count = 1
            };

            var results = new List<MessageStream>();
            await foreach (var item in client.MessageStreams.ListRecursivelyAsync(opt, requestOptions))
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
            var model = new MessageStreamCreateOptions
            {
                Name = Data!.Name,
                Type = Data!.Type,
            };

            var response = await client.MessageStreams.CreateAsync(model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task ArchiveAsync_Works(RequestOptions requestOptions)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id}/archive", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, requestOptions);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(Data!, FaluSerializerContext.Default.MessageStream),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageStreamArchiveOptions { };
            var response = await client.MessageStreams.ArchiveAsync(Data!.Id!, model, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id!, response.Resource!.Id!);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task UnarchiveAsync_Works(RequestOptions requestOptions)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id!}/unarchive", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, requestOptions);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(Data!, FaluSerializerContext.Default.MessageStream),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageStreamUnarchiveOptions { };
            var response = await client.MessageStreams.UnarchiveAsync(Data!.Id!, model, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id!, response.Resource!.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task UpdateAsync_Works(RequestOptions requestOptions)
    {
        var handler = UpdateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var document = new JsonPatchDocument<MessageStreamUpdateOptions>();
            document.Replace(x => x.Description, "new description");

            var response = await client.MessageStreams.UpdateAsync(Data!.Id!, document, requestOptions);

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
            var response = await client.MessageStreams.DeleteAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }

}
