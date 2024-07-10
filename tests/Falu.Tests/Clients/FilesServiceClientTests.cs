using Falu.Core;
using Falu.Files;
using System.Net;
using Xunit;

namespace Falu.Tests.Clients;

public class FilesServiceClientTests : BaseServiceClientTests<Files.File>
{
    public FilesServiceClientTests() : base(new()
    {
        Id = "file_123",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        Type = "image/png",
        Filename = "test.png",
        Size = 1024,
        Purpose = "customer.selfie",
    }, "/v1/files")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Files.GetAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id!, response!.Resource!.Id!);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions requestOptions, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new FilesListOptions
            {
                Count = 1
            };

            var response = await client.Files.ListAsync(opt, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource);

            if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
            else Assert.Null(response.ContinuationToken);

            var file = response!.Resource!.Single();

            Assert.Equal(Data!.Id!, file.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions requestOptions)
    {
        var handler = ListAsync_Handler(requestOptions: requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new FilesListOptions
            {
                Count = 1
            };

            var results = new List<Files.File>();

            await foreach (var item in client.Files.ListRecursivelyAsync(opt, requestOptions))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var ev = results.Single();
            Assert.Equal(Data!.Id!, ev.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions requestOptions)
    {
        var handler = CreateAsync_Handler(requestOptions, "files.falu.io");

        await TestAsync(handler, async (client) =>
        {
            var model = new FileCreateOptions
            {
                FileName = Data!.Filename,
                Content = new MemoryStream(Guid.NewGuid().ToByteArray()),
                Purpose = Data!.Purpose
            };

            var response = await client.Files.CreateAsync(model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
