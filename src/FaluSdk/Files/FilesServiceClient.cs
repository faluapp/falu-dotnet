using Falu.Core;

namespace Falu.Files;

///
public class FilesServiceClient : BaseServiceClient<File>,
                                  ISupportsListing<File, FilesListOptions>,
                                  ISupportsRetrieving<File>,
                                  ISupportsCreation<File, FileCreateRequest>
{
    ///
    public FilesServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options)
    {
    }

    ///
    protected override string BasePath => "/v1/files";

    /// <summary>List files.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<File>>> ListAsync(FilesListOptions? options = null,
                                                                RequestOptions? requestOptions = null,
                                                                CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List files recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<File> ListRecursivelyAsync(FilesListOptions? options = null,
                                                               RequestOptions? requestOptions = null,
                                                               CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>Retrieve a file.</summary>
    /// <param name="id">Unique identifier for the file.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<File>> GetAsync(string id,
                                                         RequestOptions? options = null,
                                                         CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>Upload a file.</summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<File>> CreateAsync(FileCreateRequest request,
                                                            RequestOptions? options = null,
                                                            CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));
        if (request.Purpose is null) throw new InvalidOperationException($"{nameof(request.Purpose)} cannot be null.");
        if (request.Content is null) throw new InvalidOperationException($"{nameof(request.Content)} cannot be null.");
        if (string.IsNullOrWhiteSpace(request.FileName))
        {
            throw new InvalidOperationException($"{nameof(request.FileName)} cannot be null or whitespace.");
        }

        var content = new MultipartFormDataContent
        {
            // populate fields of the model as key value pairs
            { new StringContent(request.Purpose), "purpose" },

            // populate the file stream
            { new StreamContent(request.Content), "file", request.FileName },
        };

        // Add description if provided
        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            content.Add(new StringContent(request.Description), "description");
        }

        // Add Expires if provided
        if (request.Expires is not null)
        {
            content.Add(new StringContent(request.Expires!.Value.ToString("O")), "expires");
        }

        var uri = MakePath();
        return RequestAsync<File>(uri, HttpMethod.Post, content, options, cancellationToken);
    }
}
