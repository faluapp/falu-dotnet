namespace Falu.IdentityVerifications;

/// <summary>
/// Information for creating an identity verification.
/// </summary>
public class IdentityVerificationCreateRequest : IdentityVerificationPatchModel
{
    /// <summary>
    /// A set of verification checks to be performed.
    /// </summary>
    public IdentityVerificationChecks? Checks { get; set; }

    /// <summary>
    /// The URL the user will be redirected to upon completing the verification flow.
    /// </summary>
    public string? ReturnUrl { get; set; }
}
