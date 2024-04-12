using Falu.Core;
using Falu.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Falu.Tests.Clients;

public class BaseServiceClientTests<TResource> : BaseServiceClientTests where TResource : IHasId, IHasWorkspace
{
    protected string BasePath { get; }
    protected TResource Data { get; }

    public BaseServiceClientTests(TResource data, string basePath)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
        BasePath = basePath ?? throw new ArgumentNullException(nameof(basePath));

        Data.Workspace ??= WorkspaceId;
    }

    protected DynamicHttpMessageHandler GetAsync_Handler(RequestOptions? options = null)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Get, req.Method);
            Assert.Equal($"{BasePath}/{Data.Id!}", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(Data, typeof(TResource), FaluSerializerContext.Default),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
        });

        return handler;
    }

    protected DynamicHttpMessageHandler ListAsync_Handler(bool? hasContinuationToken = null,
                                                          RequestOptions? options = null)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Get, req.Method);
            Assert.Equal($"{BasePath}", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            var content = new List<TResource> { Data };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(content, typeof(List<TResource>), FaluSerializerContext.Default),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            if (hasContinuationToken.GetValueOrDefault())
            {
                var headers = response.Headers;
                headers.Add(HeadersNames.XContinuationToken, Guid.NewGuid().ToString());
            }

            return response;
        });

        return handler;
    }

    protected DynamicHttpMessageHandler CreateAsync_Handler(RequestOptions? options = null)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(Data, typeof(TResource), FaluSerializerContext.Default),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            return response;
        });

        return handler;
    }

    protected DynamicHttpMessageHandler UpdateAsync_Handler(RequestOptions? options = null)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Patch, req.Method);
            Assert.Equal($"{BasePath}/{Data.Id!}", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(Data, typeof(TResource), FaluSerializerContext.Default),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
        });

        return handler;
    }

    protected DynamicHttpMessageHandler DeleteAsync_Handler(RequestOptions? options = null)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Delete, req.Method);
            Assert.Equal($"{BasePath}/{Data.Id!}", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK);
        });

        return handler;
    }
}

public class BaseServiceClientTests
{
    private const string TestKey = "test";

    protected const string WorkspaceId = "wksp_602a8dd0a54847479a874de4";
    protected const string IdempotencyKey = "05bc69eb-218d-46f2-8812-5bede8592abf";

    public class RequestOptionsData : TheoryData<RequestOptions>
    {
        public RequestOptionsData()
        {
            Add(new RequestOptions { });
            Add(new RequestOptions { IdempotencyKey = IdempotencyKey });
            Add(new RequestOptions { Workspace = WorkspaceId });
            Add(new RequestOptions { Live = false });
            Add(new RequestOptions { Live = true });
        }
    }

    public class RequestOptionsWithHasContinuationTokenData : TheoryData<RequestOptions, bool>
    {
        public RequestOptionsWithHasContinuationTokenData()
        {
            Add(new RequestOptions { }, false);
            Add(new RequestOptions { IdempotencyKey = IdempotencyKey }, false);
            Add(new RequestOptions { Workspace = WorkspaceId }, false);
            Add(new RequestOptions { Live = false }, true);
            Add(new RequestOptions { Live = true }, true);
        }
    }

    protected static void AssertRequestHeaders(HttpRequestMessage message, RequestOptions? options = null)
    {
        var headers = message.Headers;
        Assert.NotNull(headers.Authorization);
        Assert.True(headers.TryGetValues(HeadersNames.XFaluVersion, out _));

        if (message.Method == HttpMethod.Patch || message.Method == HttpMethod.Post)
        {
            Assert.True(headers.TryGetValues(HeadersNames.XIdempotencyKey, out _));
        }

        var idempotency_key = options?.IdempotencyKey;
        if (!string.IsNullOrWhiteSpace(idempotency_key))
        {
            Assert.True(headers.TryGetValues(HeadersNames.XIdempotencyKey, out IEnumerable<string>? values));
            Assert.Equal(idempotency_key!, values!.First());
        }

        var workspace = options?.Workspace;
        if (!string.IsNullOrWhiteSpace(workspace))
        {
            Assert.True(headers.TryGetValues(HeadersNames.XWorkspaceId, out IEnumerable<string>? values));
            Assert.Equal(workspace!, values!.First());
        }

        var live = options?.Live;
        if (live.HasValue)
        {
            Assert.True(headers.TryGetValues(HeadersNames.XLiveMode, out IEnumerable<string>? values));
            Assert.Equal(live!, bool.Parse(values!.First()));
        }
    }

    protected static async Task TestAsync(HttpMessageHandler handler, Func<FaluClient, Task> logic)
    {
        var services = new ServiceCollection();
        services.AddFalu(TestKey)
                .ConfigurePrimaryHttpMessageHandler(() => handler);

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var sp = scope.ServiceProvider;

        var svc = sp.GetRequiredService<FaluClient>();

        await logic(svc);
    }
}
