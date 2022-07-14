namespace Falu.IdentityVerifications;

/// <summary>
/// Information for creating an identity verification.
/// </summary>
public class IdentityVerificationCreateRequest : IdentityVerificationPatchModel
{
    /// <summary>
    /// The type of verification check to be performed.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// A set of verification checks to be performed.
    /// </summary>
    public IdentityVerificationOptions? Options { get; set; }

    /// <summary>
    /// The URL the user will be redirected to upon completing the verification flow.
    /// </summary>
    public string? ReturnUrl { get; set; }
}
