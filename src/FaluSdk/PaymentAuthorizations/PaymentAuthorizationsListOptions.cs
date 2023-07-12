using Falu.Core;

namespace Falu.PaymentAuthorizations;

/// <summary>Options for filtering and pagination of payment authorizations.</summary>
public record PaymentAuthorizationsListOptions : BasicListOptionsWithMoney
{
    /// <summary>Filter options for <see cref="PaymentAuthorization.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <summary>Filter options for <see cref="PaymentAuthorization.StatusReason"/> property.</summary>
    public List<string>? StatusReason { get; set; }

    /// <summary>
    /// Filter options for <c>decline_reason</c> property.
    /// <summary>Filter options for <see cref="PaymentAuthorization.DeclineReason"/> property.</summary>
    /// </summary>
    public List<string>? DeclineReason { get; set; }

    /// <summary>Filter options for <see cref="PaymentAuthorization.Approved"/> property.</summary>
    public bool? Approved { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status)
              .Add("approved", Approved)
              .Add("status_reason", StatusReason)
              .Add("decline_reason", DeclineReason);
    }
}
