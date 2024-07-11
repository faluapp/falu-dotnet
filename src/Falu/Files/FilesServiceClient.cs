using Falu.Core;

namespace Falu.Files;

///
public class FilesServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<File>(backChannel, options),
                                                                                     ISupportsListing<File, FilesListOptions>,
                                                                                     ISupportsRetrieving<File>,
                                                                                     ISupportsCreation<File, FileCreateOptions>
{
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
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<File>> GetAsync(string id,
                                                         RequestOptions? requestOptions = null,
                                                         CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, requestOptions, cancellationToken);
    }

    /// <summary>Upload a file.</summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<File>> CreateAsync(FileCreateOptions options,
                                                            RequestOptions? requestOptions = null,
                                                            CancellationToken cancellationToken = default)
    {
        if (options is null) throw new ArgumentNullException(nameof(options));
        if (options.Purpose is null) throw new InvalidOperationException($"{nameof(options.Purpose)} cannot be null.");
        if (options.Content is null) throw new InvalidOperationException($"{nameof(options.Content)} cannot be null.");
        if (string.IsNullOrWhiteSpace(options.FileName))
        {
            throw new InvalidOperationException($"{nameof(options.FileName)} cannot be null or whitespace.");
        }

        var content = new MultipartFormDataContent
        {
            // populate fields of the model as key value pairs
            { new StringContent(options.Purpose), "purpose" },

            // populate the file stream
            { new StreamContent(options.Content), "file", options.FileName },
        };

        // Add description if provided
        if (!string.IsNullOrWhiteSpace(options.Description))
        {
            content.Add(new StringContent(options.Description), "description");
        }

        // Add Expires if provided
        if (options.Expires is not null)
        {
            content.Add(new StringContent(options.Expires!.Value.ToString("O")), "expires");
        }

        var uri = new UriBuilder(BackChannel.BaseAddress!) { Host = Options.FilesHost, Path = MakePath() }.ToString();
        return RequestResourceAsync(uri, HttpMethod.Post, content, requestOptions, cancellationToken);
    }
}
