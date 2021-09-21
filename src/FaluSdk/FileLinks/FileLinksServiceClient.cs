using Falu.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.FileLinks
{
    ///
    public class FileLinksServiceClient : BaseServiceClient<FileLink>, ISupportsListing<FileLink, FileLinksListOptions>
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
        /// <param name="link"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<FileLink>> CreateAsync(FileLinkCreateRequest link,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(link, options, cancellationToken);
        }

        /// <summary>Update a file link.</summary>
        /// <param name="id">Unique identifier for the file.</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<FileLink>> UpdateAsync(string id,
                                                                    JsonPatchDocument<FileLinkPatchModel> patch,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }
    }
}
