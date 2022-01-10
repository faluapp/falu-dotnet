using Falu.Core;

namespace Falu.Files;

///
public class FilesServiceClient : BaseServiceClient<File>, ISupportsListing<File, FilesListOptions>
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
    /// <param name="file"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<File>> CreateAsync(FileCreateRequest file,
                                                            RequestOptions? options = null,
                                                            CancellationToken cancellationToken = default)
    {
        if (file is null) throw new ArgumentNullException(nameof(file));
        if (file.Purpose is null) throw new InvalidOperationException($"{nameof(file.Purpose)} cannot be null.");
        if (file.Content is null) throw new InvalidOperationException($"{nameof(file.Content)} cannot be null.");
        if (string.IsNullOrWhiteSpace(file.FileName))
        {
            throw new InvalidOperationException($"{nameof(file.FileName)} cannot be null or whitespace.");
        }

        var content = new MultipartFormDataContent
        {
            // populate fields of the model as key value pairs
            { new StringContent(file.Purpose), "purpose" },

            // populate the file stream
            { new StreamContent(file.Content), "file", file.FileName },
        };

        // Add description if provided
        if (!string.IsNullOrWhiteSpace(file.Description))
        {
            content.Add(new StringContent(file.Description), "description");
        }

        // Add Expires if provided
        if (file.Expires is not null)
        {
            content.Add(new StringContent(file.Expires!.Value.ToString("O")), "expires");
        }

        var uri = MakePath();
        return RequestAsync<File>(uri, HttpMethod.Post, content, options, cancellationToken);
    }
}
