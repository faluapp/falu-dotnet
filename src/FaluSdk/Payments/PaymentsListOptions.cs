using Falu.Core;

namespace Falu.Payments;

/// <summary>Options for filtering and pagination of payments.</summary>
public record PaymentsListOptions : BasicListOptionsWithMoney
{
    /// <summary>Filter options for <see cref="Payment.Status"/> property.</summary>
    public List<PaymentStatus>? Status { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status);
    }
}
