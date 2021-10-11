﻿using Falu.Core;
using Falu.FileLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients
{
    public class FileLinksServiceClientTests : BaseServiceClientTests<FileLink>
    {
        public FileLinksServiceClientTests() : base(new()
        {
            Id = "link_123",
            Created = DateTimeOffset.UtcNow,
            Updated = DateTimeOffset.UtcNow,
            FileId = "file_123",
            Url = "https://test.falu.io",
            WorkspaceId = WorkspaceId
        }, "/v1/file_links") { }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task GetAsync_Works(RequestOptions options)
        {
            var handler = GetAsync_Handler(options);

            await TestAsync(handler, async (client) =>
            {
                var response = await client.FileLinks.GetAsync(Data!.Id!, options);
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
                var opt = new FileLinksListOptions
                {
                    Count = 1
                };

                var response = await client.FileLinks.ListAsync(opt, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
                Assert.Single(response.Resource);

                if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
                else Assert.Null(response.ContinuationToken);

                var flk = response!.Resource!.Single();

                Assert.Equal(Data!.Id, flk.Id);
            });
        }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task ListRecursivelyAsync_Works(RequestOptions options)
        {
            var handler = ListAsync_Handler(options: options);

            await TestAsync(handler, async (client) =>
            {
                var opt = new FileLinksListOptions
                {
                    Count = 1
                };

                var results = new List<FileLink>();

                await foreach (var item in client.FileLinks.ListRecursivelyAsync(opt, options))
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
                var model = new FileLinkCreateRequest 
                {
                    FileId = Data!.FileId
                };

                var response = await client.FileLinks.CreateAsync(model, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
                Assert.Equal(Data!.FileId, response.Resource!.FileId);
            });
        }

        [Theory]
        [MemberData(nameof(RequestOptionsData))]
        public async Task UpdateAsync_Works(RequestOptions options)
        {
            var handler = UpdateAsync_Handler(options);

            await TestAsync(handler, async (client) =>
            {
                var document = new JsonPatchDocument<FileLinkPatchModel>();
                document.Replace(x => x.Expires, null);

                var response = await client.FileLinks.UpdateAsync(Data!.Id!, document, options);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(response.Resource);
            });
        }

    }
}