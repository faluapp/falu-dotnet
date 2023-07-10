using Falu.Core;

namespace Falu.PaymentAuthorizations;

/// <summary>Options for filtering and pagination of payment authorizations.</summary>
public record PaymentAuthorizationsListOptions : BasicListOptionsWithMoney
{
    /// <summary>Filter options for <see cref="PaymentAuthorization.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <summary>Filter options for <see cref="PaymentAuthorization.Approved"/> property.</summary>
    public bool? Approved { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status)
              .Add("approved", Approved);
    }
}
