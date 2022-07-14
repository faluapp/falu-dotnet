namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportSelfie : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Identifier of the file holding the image of the identity document used in this check.
    /// </summary>
    public string? Document { get; set; }

    /// <summary>
    /// Identifier of the file holding the image of the selfie used in this check.
    /// </summary>
    public string? Selfie { get; set; }
}
