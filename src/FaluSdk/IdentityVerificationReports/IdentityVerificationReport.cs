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
    /// Result from a video check.
    /// </summary>
    public IdentityVerificationReportVideo? Video { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
