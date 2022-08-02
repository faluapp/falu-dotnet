using Falu.Core;

namespace Falu.PaymentRefunds;

/// <summary>Options for filtering and pagination of list payment refunds operation.</summary>
public record PaymentRefundsListOptions : BasicListOptionsWithMoney
{
    /// <summary>Filter options for <see cref="PaymentRefund.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <summary>Filter options for <see cref="PaymentRefund.Reason"/> property.</summary>
    public List<string>? Reason { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status)
              .Add("reason", Reason);
    }
}
