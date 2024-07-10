namespace Falu;

/// <summary>
/// Modifies the request uri for file uploads to use files.falu.io host
/// </summary>
internal class FilesUploadHandler : DelegatingHandler
{
    private readonly Uri FilesUploadUri = new("https://files.falu.io/v1/files");

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var path = request.RequestUri!.AbsolutePath;
        if (request.Method == HttpMethod.Post && FilesUploadUri.AbsolutePath.Equals(path))
        {
            // Send the request to files.falu.io
            request.RequestUri = FilesUploadUri;
        }

        return base.SendAsync(request, cancellationToken);
    }
}
