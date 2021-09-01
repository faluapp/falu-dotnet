using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.FileUploadLinks
{
    /// <summary>
    /// A model representing details that can be changed about a file upload link.
    /// </summary>
    public class FileUploadLinkPatchModel : IHasMetadata
    {
        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }

        /// <summary>
        /// Time at which the link expires.
        /// </summary>
        public DateTimeOffset? Expires { get; set; }
    }
}
