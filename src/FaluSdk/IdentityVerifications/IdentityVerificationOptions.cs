namespace Falu.IdentityVerifications;

///
public class IdentityVerificationOptions
{
    /// <summary>
    /// Options for the id number check.
    /// </summary>
    public IdentityVerificationOptionsForIdNumber? IdNumber { get; set; }

    /// <summary>
    /// Options for the document check.
    /// </summary>
    public IdentityVerificationOptionsForDocument? Document { get; set; }
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
    /// Disable image uploads, identity document images have to be captured using the device's camera.
    /// </summary>
    public bool Live { get; set; }

    /// <summary>
    /// The allowed identity document types.
    /// If a user uploads a document which isn't one of the allowed types, it will be rejected.
    /// </summary>
    public List<string>? Allowed { get; set; }
}
