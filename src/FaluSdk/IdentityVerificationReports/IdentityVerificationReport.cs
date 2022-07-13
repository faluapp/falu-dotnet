using Falu.Core;
using Falu.IdentityVerifications;

namespace Falu.IdentityVerificationReports;

/// <summary>An identity verification report.</summary>
public class IdentityVerificationReport : IHasId, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// The checks that initiated this report.
    /// </summary>
    public IdentityVerificationChecks? Checks { get; set; }

    /// <summary>
    /// Details on the user’s acceptance of the Services Agreement.
    /// </summary>
    public IdentityVerificationConsent? Consent { get; set; }

    /// <summary>
    /// Result from an id number check.
    /// </summary>
    public IdentityVerificationReportIdNumber? IdNumber { get; set; }

    /// <summary>
    /// Result from a document check.
    /// </summary>
    public IdentityVerificationReportDocument? Document { get; set; }

    /// <summary>
    /// Result from a selfie check.
    /// </summary>
    public IdentityVerificationReportSelfie? Selfie { get; set; }

    /// <summary>
    /// Result from a liveness check.
    /// </summary>
    public IdentityVerificationReportLiveness? Liveness { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}

///
public class IdentityVerificationConsent
{
    /// <summary>
    /// The timestamp marking when the user gave consent for the identity verification to be done.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// The IP address from which the user gave consent for the identity verification to be done.
    /// </summary>
    /// <example>::ffff:127.0.0.1</example>
    public string? IP { get; set; }

    /// <summary>
    /// The user agent of the browser from which the user gave consent for the identity verification to be done.
    /// </summary>
    /// <example>Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36</example>
    public string? UserAgent { get; set; }
}

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

///
public class IdentityVerificationReportDocument : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Expiry date of the document.
    /// </summary>
    public DateTimeOffset? Expiry { get; set; }

    /// <summary>
    /// Issued date of the document.
    /// </summary>
    public DateTimeOffset? Issued { get; set; }

    /// <summary>
    /// Three-letter <see href="https://www.iso.org/iso-3166-country-codes.html">ISO country code</see>,
    /// in lowercase, which issued the document.
    /// </summary>
    /// <example>ken</example>
    public string? Issuer { get; set; }

    /// <summary>
    /// Three-letter <see href="https://www.iso.org/iso-3166-country-codes.html">ISO country code</see>,
    /// in lowercase, where the entity issued the document originates from.
    /// </summary>
    /// <example>ken</example>
    public string? Nationality { get; set; }

    /// <summary>
    /// Type of the document.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Sub type for the document
    /// </summary>
    public string? SubType { get; set; }

    /// <summary>
    /// Document identification number.
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Personal number
    /// </summary>
    public string? PersonalNumber { get; set; }

    /// <summary>
    /// First name as it appears in the document.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name as it appears in the document.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Other names as they appear in the document.
    /// </summary>
    public List<string>? OtherNames { get; set; }

    /// <summary>
    /// Sex as it appears on the document
    /// </summary>
    public string? Sex { get; set; }

    /// <summary>
    /// Date of birth as it appears in the document.
    /// </summary>
    public DateTimeOffset? Birthday { get; set; }

    /// <summary>
    /// Address as it appears in the document.
    /// </summary>
    public PhysicalAddress? Address { get; set; }

    /// <summary>
    /// Unique identifiers of the files containing images for this document.
    /// </summary>
    public List<string> Files { get; set; } = new List<string>();
}

///
public class IdentityVerificationReportSelfie : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Identifier of the file holding the image of the identity document used in this check.
    /// </summary>
    public string? Document { get; set; }

    /// <summary>
    /// Identifier of the file holding the image of the selfie used in this check.
    /// </summary>
    public string? Selfie { get; set; }
}

///
public class IdentityVerificationReportLiveness : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// Identifier of the file holding the video used in this check.
    /// </summary>
    public string? Video { get; set; }

    /// <summary>
    /// Identifier of the file holding the image of the potrait used in this check.
    /// </summary>
    public string? Portrait { get; set; }
}

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

///
public class IdentityVerificationReportError
{
    /// <summary>
    /// A short machine-readable string giving the reason for the verification failure.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// A human-readable message giving the reason for the failure.
    /// These message can be shown to your user.
    /// </summary>
    public string? Description { get; set; }
}
