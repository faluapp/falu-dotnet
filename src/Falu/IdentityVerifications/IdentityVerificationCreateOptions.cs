using Falu.Core;

namespace Falu.IdentityVerifications;

/// <summary>
/// Information for creating an identity verification.
/// </summary>
public class IdentityVerificationCreateOptions : IHasDescription, IHasMetadata
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

    /// <summary>
    /// Identifier of the Customer this Identity Verification belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    public virtual string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
