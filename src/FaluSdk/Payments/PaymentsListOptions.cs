﻿using Falu.Core;

namespace Falu.Payments;

/// <summary>Options for filtering and pagination of payments.</summary>
public record PaymentsListOptions : BasicListOptionsWithMoney
{
    /// <summary>Filter options for <see cref="Payment.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <summary>Filter options for <see cref="Payment.Type"/> property.</summary>
    public List<string>? Type { get; set; }

    /// <summary>Filter options for <see cref="Payment.Customer"/> property.</summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status)
              .Add("type", Type)
              .Add("customer", Customer);
    }
}
