using Falu.Core;

namespace Falu.IdentityVerifications;

/// <summary>
/// A model representing details that can be changed about an identity verification.
/// </summary>
public class IdentityVerificationUpdateOptions : IHasDescription, IHasMetadata
{
    /// <inheritdoc/>
    public virtual string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
