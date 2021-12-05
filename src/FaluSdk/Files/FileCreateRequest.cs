using Falu.Core;

namespace Falu.Files
{
    /// <summary>Information for creating a file.</summary>
    public class FileCreateRequest : IHasDescription
    {
        /// <summary>Name of the file.</summary>
        public string? FileName { get; set; }

        /// <summary>Contents of the file.</summary>
        public Stream? Content { get; set; }

        /// <summary>Purpose of the file.</summary>
        public FilePurpose? Purpose { get; set; }

        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <summary>Time at which the file expires.</summary>
        public DateTimeOffset? Expires { get; set; }
    }
}
