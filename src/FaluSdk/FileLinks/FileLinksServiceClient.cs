using Falu.Core;
using System.Net.Http.Json;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.FileLinks;

///
public class FileLinksServiceClient(HttpClient backChannel, FaluClientOptions options) : BaseServiceClient<FileLink>(backChannel, options),
                                                                                         ISupportsListing<FileLink, FileLinksListOptions>,
                                                                                         ISupportsRetrieving<FileLink>,
                                                                                         ISupportsCreation<FileLink, FileLinkCreateRequest>,
                                                                                         ISupportsUpdating<FileLink, FileLinkPatchModel>
{
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
    public virtual Task<ResourceResponse<FileLink>> CreateAsync(FileLinkCreateRequest request,
                                                                RequestOptions? options = null,
                                                                CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.FileLinkCreateRequest);
        return CreateResourceAsync(content, options, cancellationToken);
    }

    /// <summary>Update a file link.</summary>
    /// <param name="id">Unique identifier for the file.</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<FileLink>> UpdateAsync(string id,
                                                                JsonPatchDocument<FileLinkPatchModel> request,
                                                                RequestOptions? options = null,
                                                                CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, SC.Default.JsonPatchDocumentFileLinkPatchModel);
        return UpdateResourceAsync(id, content, options, cancellationToken);
    }
}
