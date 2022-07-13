namespace Falu.IdentityVerifications;

///
public class IdentityVerificationChecks
{
    /// <summary>
    /// Options for the id number check.
    /// </summary>
    public IdentityVerificationChecksForIdNumber? IdNumber { get; set; }

    /// <summary>
    /// Options for the document check.
    /// </summary>
    public IdentityVerificationChecksForDocument? Document { get; set; }
}

///
public class IdentityVerificationChecksForIdNumber
{
    // intentionally left blank
}

///
public class IdentityVerificationChecksForDocument
{
    /// <summary>
    /// Disable image uploads, identity document images have to be captured using the device's camera.
    /// </summary>
    public bool LiveCapture { get; set; }

    /// <summary>
    /// The allowed identity document types.
    /// If a user uploads a document which isn't one of the allowed types, it will be rejected.
    /// </summary>
    public List<string>? Allowed { get; set; }
}
