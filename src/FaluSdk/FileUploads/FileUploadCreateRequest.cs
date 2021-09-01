using Falu.Core;
using System;
using System.IO;

namespace Falu.FileUploads
{
    /// <summary>
    /// Information for creating a file upload.
    /// </summary>
    public class FileUploadCreateRequest : IHasDescription
    {
        /// <summary>
        /// Name of the file.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Contents of the file.
        /// </summary>
        public Stream? Content { get; set; }

        /// <summary>
        /// Purpose of the uploaded file.
        /// </summary>
        public FileUploadPurpose? Purpose { get; set; }

        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <summary>
        /// Time at which the file upload expires.
        /// </summary>
        public DateTimeOffset? Expires { get; set; }
    }
}
