using Falu.Core;

namespace Falu.IdentityVerifications;

/// <summary>Options for filtering and pagination of identity verifications.</summary>
public record IdentityVerificationsListOptions : BasicListOptions
{
    /// <summary>Filter options for <see cref="IdentityVerification.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <summary>Filter options for <see cref="IdentityVerification.Customer"/> property.</summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status)
              .Add("customer", Customer);
    }
}
