using Falu.Core;

namespace Falu.IdentityVerificationReports;

/// <summary>Options for filtering and pagination of identity verification reports.</summary>
public record IdentityVerificationReportsListOptions : BasicListOptions
{
    /// <summary>
    /// Unique identifier of the identity verification to filter for.
    /// </summary>
    public string? Verification { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("verification", Verification);
    }
}
