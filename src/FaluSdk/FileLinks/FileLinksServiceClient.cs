using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.FileLinks;

///
public class FileLinksServiceClient : BaseServiceClient<FileLink>,
                                      ISupportsListing<FileLink, FileLinksListOptions>,
                                      ISupportsRetrieving<FileLink>,
                                      ISupportsCreation<FileLink, FileLinkCreateRequest>,
                                      ISupportsUpdating<FileLink, FileLinkPatchModel>
{
    ///
    public FileLinksServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options)
    {
    }

    ///
    protected override string BasePath => "/v1/file_links";

    /// <summary>List file links.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<FileLink>>> ListAsync(FileLinksListOptions? options = null,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List file links recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<FileLink> ListRecursivelyAsync(FileLinksListOptions? options = null,
                                                                   RequestOptions? requestOptions = null,
                                                                   CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>Retrieve a file link.</summary>
    /// <param name="id">Unique identifier for the file link.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<FileLink>> GetAsync(string id,
                                                             RequestOptions? options = null,
                                                             CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>Create a file link.</summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<FileLink>> CreateAsync(FileLinkCreateRequest request,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.FileLinkCreateRequest, cancellationToken).ConfigureAwait(false);
        return await CreateResourceAsync(content, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Update a file link.</summary>
    /// <param name="id">Unique identifier for the file.</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<FileLink>> UpdateAsync(string id,
                                                                      JsonPatchDocument<FileLinkPatchModel> request,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.JsonPatchDocumentFileLinkPatchModel, cancellationToken).ConfigureAwait(false);
        return await UpdateResourceAsync(id, content, options, cancellationToken).ConfigureAwait(false);
    }
}
