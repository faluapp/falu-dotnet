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
    public IdentityVerificationOptions? Options { get; set; }

    /// <summary>
    /// Details on the user’s acceptance of the Services Agreement.
    /// </summary>
    public IdentityVerificationReportConsent? Consent { get; set; }

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
    /// Result from a video check.
    /// </summary>
    public IdentityVerificationReportVideo? Video { get; set; }

    /// <summary>
    /// Result from the IP address check.
    /// </summary>
    public IdentityVerificationReportIPAddress? IPAddress { get; set; }

    /// <summary>
    /// Result from the device check.
    /// </summary>
    public IdentityVerificationReportDevice? Device { get; set; }

    /// <summary>
    /// Result from tax id check
    /// </summary>
    public IdentityVerificationReportTaxId? TaxId { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
