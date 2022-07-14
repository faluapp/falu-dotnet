namespace Falu.IdentityVerificationReports;

///
public abstract class AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Details on the verification error.
    /// Present when not verified.
    /// </summary>
    public IdentityVerificationReportError? Error { get; set; }

    /// <summary>
    /// Whether the check resulted in a successful verification.
    /// </summary>
    public bool Verified { get; set; }
}
