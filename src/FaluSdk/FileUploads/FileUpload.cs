using Falu.Core;
using System;

namespace Falu.FileUploads
{
    /// <summary>
    /// A file upload link.
    /// </summary>
    public class FileUpload : IHasId, IHasCreated, IHasUpdated, IHasDescription, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string? Id { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <summary>
        /// Purpose of the uploade file
        /// </summary>
        public FileUploadPurpose Purpose { get; set; }

        /// <summary>
        /// Type of file uploaded.
        /// </summary>
        /// <example>image/png</example>
        public string? Type { get; set; }

        /// <summary>
        /// A name of the upload suitable for saving to a filesystem.
        /// </summary>
        public string? Filename { get; set; }

        /// <summary>
        /// Size in bytes of the uploaded file.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Time at which the upload expires.
        /// </summary>
        public DateTimeOffset? Expires { get; set; }

        /// <inheritdoc/>
        public string? WorkspaceId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string? Etag { get; set; }
    }
}
