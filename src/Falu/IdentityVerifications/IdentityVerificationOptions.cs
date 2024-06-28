namespace Falu.IdentityVerifications;

///
public class IdentityVerificationOptions
{
    /// <summary>
    /// Whether to allow uploads for documents, images and videos.
    /// This only applies to document related checks.
    /// </summary>
    public bool? AllowUploads { get; set; }

    /// <summary>
    /// Options for the id number check.
    /// </summary>
    public IdentityVerificationOptionsForIdNumber? IdNumber { get; set; }

    /// <summary>
    /// Options for the document check.
    /// </summary>
    public IdentityVerificationOptionsForDocument? Document { get; set; }

    /// <summary>
    /// Options for the selfie check.
    /// </summary>
    public IdentityVerificationOptionsForSelfie? Selfie { get; set; }

    /// <summary>
    /// Options for the video check.
    /// </summary>
    public IdentityVerificationOptionsForVideo? Video { get; set; }
}

///
public class IdentityVerificationOptionsForIdNumber
{
    // intentionally left blank
}

///
public class IdentityVerificationOptionsForDocument
{
    /// <summary>
    /// The allowed identity document types.
    /// If a user uploads a document which isn't one of the allowed types, it will be rejected.
    /// </summary>
    public List<string>? Allowed { get; set; }
}

///
public class IdentityVerificationOptionsForSelfie
{
    // intentionally left blank
}

///
public class IdentityVerificationOptionsForVideo
{
    /// <summary>
    /// Face poses to be performed in the video recording.
    /// It is recommended to leave this field unassigned for the server to
    /// generate random values per verification for security purposes.
    /// </summary>
    public List<string>? Poses { get; set; }

    /// <summary>
    /// Numerical phrase to be recited in the video recording.
    /// When not provided, the server generates a random one.
    /// </summary>
    public int? Recital { get; set; }
}
