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
        internal override void Populate(QueryValues values)
        {
            base.Populate(values);
            values.Add("purpose", Purpose);
        }
    }
}
