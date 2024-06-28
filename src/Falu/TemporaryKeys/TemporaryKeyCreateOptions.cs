namespace Falu.TemporaryKeys;

/// <summary>
/// Information for creating a temporary key
/// </summary>
public class TemporaryKeyCreateOptions
{
    /// <summary>
    /// Unique identifier of the identity verification to be accessed using the key.
    /// </summary>
    public string? IdentityVerification { get; set; }
}
