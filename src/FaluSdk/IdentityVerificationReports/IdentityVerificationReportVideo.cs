namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportVideo : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Identifier of the file holding the image of the identity document used in this check.
    /// </summary>
    public string? Document { get; set; }

    /// <summary>
    /// Identifier of the file holding the video used in this check.
    /// </summary>
    public string? Video { get; set; }

    /// <summary>
    /// Identifier of the file holding the image of the potrait used in this check.
    /// </summary>
    public string? Portrait { get; set; }
}
