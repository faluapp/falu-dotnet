namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportIdNumber : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Type of ID number.
    /// </summary>
    public string? IdNumberType { get; set; }

    /// <summary>
    /// Identification number.
    /// </summary>
    public string? IdNumber { get; set; }

    /// <summary>
    /// Three-letter <see href="https://www.iso.org/iso-3166-country-codes.html">ISO country code</see>,
    /// in lowercase, which issued the document.
    /// </summary>
    /// <example>ken</example>
    public string? Issuer { get; set; }

    /// <summary>
    /// The first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The date of birth.
    /// </summary>
    public DateTimeOffset? Birthday { get; set; }

    /// <summary>
    /// The other names.
    /// </summary>
    public List<string>? OtherNames { get; set; }

    /// <summary>
    /// The sex as it should appear on the document
    /// </summary>
    public string? Sex { get; set; }
}
