using Falu.Core;
using System.Collections.Generic;

namespace Falu.FileUploadLinks
{
    /// <summary>
    /// Options for filtering and pagination of list flie upload links.
    /// </summary>
    public record FileUploadLinksListOptions : BasicListOptions
    {
        /// <summary>
        /// Unique identifier of the file upload.
        /// </summary>
        public string? FileUpload { get; set; }

        /// <summary>
        /// Filter options for <code>expired</code> property.
        /// </summary>
        public bool? Expired { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("upload", FileUpload);
            dictionary.AddIfNotNull("expired", Expired, ConvertBool);

            return dictionary;
        }
    }
}
