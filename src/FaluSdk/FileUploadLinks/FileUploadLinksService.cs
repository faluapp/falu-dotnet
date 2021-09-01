using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.FileUploadLinks
{
    ///
    public class FileUploadLinksService : BaseService<FileUploadLink>
    {
        ///
        public FileUploadLinksService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options)
        {
        }

        ///
        protected override string BasePath => "/v1/file_upload_links";

        /// <summary>List file upload links.</summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<FileUploadLink>>> ListAsync(FileUploadLinksListOptions? options = null,
                                                                              RequestOptions? requestOptions = null,
                                                                              CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>Retrieve a file upload link.</summary>
        /// <param name="id">Unique identifier for the file upload link.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<FileUploadLink>> GetAsync(string id,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>Create a file upload link.</summary>
        /// <param name="link"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<FileUploadLink>> CreateAsync(FileUploadLinkCreateRequest link,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(link, options, cancellationToken);
        }

        /// <summary>Update a file upload link.</summary>
        /// <param name="id">Unique identifier for the message stream</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<FileUploadLink>> UpdateAsync(string id,
                                                                          JsonPatchDocument<FileUploadLinkPatchModel> patch,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }
    }
}
