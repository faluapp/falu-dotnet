namespace Falu.IdentityVerifications;

///
public class IdentityVerificationLastError
{
    /// <summary>
    /// A short machine-readable string giving the reason for verification or user-session failure.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// A human-readable message that explains the reason for verification or user-session failure.
    /// These message can be shown to your user.
    /// </summary>
    public string? Description { get; set; }
}
