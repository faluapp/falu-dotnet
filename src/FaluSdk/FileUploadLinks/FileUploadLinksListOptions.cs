using Falu.Core;

namespace Falu.FileUploadLinks
{
    /// <summary>Options for filtering and pagination of list flie upload links.</summary>
    public record FileUploadLinksListOptions : BasicListOptions
    {
        /// <summary>Unique identifier of the file upload.</summary>
        public string? FileUpload { get; set; }

        /// <summary>Filter options for <see cref="FileUploadLink.Expired"/> property.</summary>
        public bool? Expired { get; set; }

        /// <inheritdoc/>
        internal override void Populate(QueryValues values)
        {
            base.Populate(values);
            values.Add("upload", FileUpload)
                  .Add("expired", Expired);
        }
    }
}
