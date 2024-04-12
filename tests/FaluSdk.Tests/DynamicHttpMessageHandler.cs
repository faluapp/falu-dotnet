namespace Falu.Tests;

public class DynamicHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> processFunc) : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> processFunc = processFunc ?? throw new ArgumentNullException(nameof(processFunc));

    public DynamicHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, HttpResponseMessage> processFunc)
        : this((req, ct) => Task.FromResult(processFunc(req, ct))) { }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => processFunc(request, cancellationToken);
}
