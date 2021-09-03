using Falu.Core;
using System.Collections.Generic;

namespace Falu.FileUploads
{
    /// <summary>Options for filtering and pagination of list file uploads.</summary>
    public record FileUploadsListOptions : BasicListOptions
    {
        /// <summary>Filter options for <code>purpose</code> property.</summary>
        public List<FileUploadPurpose>? Purpose { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("purpose", Purpose, ConvertEnumList);

            return dictionary;
        }
    }
}
