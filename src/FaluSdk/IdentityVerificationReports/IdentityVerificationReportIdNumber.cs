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
