namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportError
{
    /// <summary>
    /// A short machine-readable string giving the reason for the verification failure.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// A human-readable message giving the reason for the failure.
    /// These message can be shown to your user.
    /// </summary>
    public string? Description { get; set; }
}
