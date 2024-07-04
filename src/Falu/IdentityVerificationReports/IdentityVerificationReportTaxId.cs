using Falu.Core;

namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportTaxId : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// The tax id type
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Status of the tax identification number.
    /// </summary>
    public string? TaxStatus { get; set; }

    /// <summary>
    /// Value of the tax id
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// The verified name of the tax payer
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The verified address of the customer
    /// </summary>
    public PhysicalAddress? Address { get; set; }

    /// <summary>
    /// The verified registered tax obligations
    /// </summary>
    public List<IdentityVerificationTaxIdObligation>? Obligations { get; set; }
}

///
public class IdentityVerificationTaxIdObligation 
{
    /// <summary>The Name.</summary>
    /// <example>Value Added Tax (VAT)</example>
    public string? Name { get; set; }

    /// <summary>
    /// The status of the tax obligation
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The tax obligation registration dates
    /// </summary>
    public IdentityVerificationTaxIdObligationPeriod? Effective { get; set; }
}

///
public class IdentityVerificationTaxIdObligationPeriod 
{
    /// <summary>The starting date of the effective period.</summary>
    /// <example>2019-08-24T14:15:22Z</example>
    public DateTimeOffset Start { get; set; }

    /// <summary>
    /// The ending date of the effective period.
    /// When null, it means the obligation is still effective.
    /// </summary>
    /// <example>2019-09-24T14:15:22Z</example>
    public DateTimeOffset? End { get; set; }
}
