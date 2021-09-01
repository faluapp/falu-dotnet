using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.FileUploads
{
    ///
    public class FileUploadsService : BaseService<FileUpload>, ISupportsListing<FileUpload, FileUploadsListOptions>
    {
        ///
        public FileUploadsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options)
        {
        }

        ///
        protected override string BasePath => "/v1/file_uploads";

        /// <summary>List file uploads.</summary>
        /// <inheritdoc/>
        public virtual Task<ResourceResponse<List<FileUpload>>> ListAsync(FileUploadsListOptions? options = null,
                                                                          RequestOptions? requestOptions = null,
                                                                          CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>List file uploads recursively.</summary>
        /// <inheritdoc/>
        public virtual IAsyncEnumerable<FileUpload> ListRecursivelyAsync(FileUploadsListOptions? options = null,
                                                                         RequestOptions? requestOptions = null,
                                                                         CancellationToken cancellationToken = default)
        {
            return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>Retrieve a file upload.</summary>
        /// <param name="id">Unique identifier for the file upload.</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<FileUpload>> GetAsync(string id,
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
        public virtual Task<ResourceResponse<FileUpload>> CreateAsync(FileUploadCreateRequest file,
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
                { new StringContent(file.Purpose?.GetEnumMemberAttrValueOrDefault()), "purpose" },

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
            return RequestAsync<FileUpload>(uri, HttpMethod.Post, content, options, cancellationToken);
        }
    }
}
