using Falu.Core;

namespace Falu.IdentityVerifications;

///
public class IdentityVerificationOutputs
{
    /// <summary>
    /// The user’s verified id number type.
    /// </summary>
    public string? IdNumberType { get; set; }

    /// <summary>
    /// The user’s verified id number.
    /// </summary>
    public string? IdNumber { get; set; }

    /// <summary>
    /// Three-letter <see href="https://www.iso.org/iso-3166-country-codes.html">ISO country code</see>,
    /// in lowercase, which issued the document used, if any.
    /// </summary>
    /// <example>ken</example>
    public string? Issuer { get; set; }

    /// <summary>
    /// The user’s verified first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The user’s verified last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The user’s verified date of birth.
    /// </summary>
    public DateTimeOffset? Birthday { get; set; }

    /// <summary>
    /// The user’s other verified names.
    /// </summary>
    public List<string>? OtherNames { get; set; }

    /// <summary>
    /// The user’s verified address.
    /// </summary>
    public PhysicalAddress? Address { get; set; }

    /// <summary>
    /// The user's sex 
    /// </summary>
    public string? Sex { get; set; }
}
