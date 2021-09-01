namespace Falu.FileUploadLinks
{
    /// <summary>
    /// Information for creating a file upload link.
    /// </summary>
    public class FileUploadLinkCreateRequest : FileUploadLinkPatchModel
    {
        /// <summary>
        /// Unique identifier of the file upload.
        /// </summary>
        public string? FileUploadId { get; set; }
    }
}
