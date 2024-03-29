﻿using Falu.Core;

namespace Falu.TransferReversals;

/// <summary>Options for filtering and pagination of transfer reversals.</summary>
public record TransferReversalsListOptions : BasicListOptionsWithMoney
{
    /// <summary>Filter options for <see cref="TransferReversal.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <summary>Filter options for <see cref="TransferReversal.Reason"/> property.</summary>
    public List<string>? Reason { get; set; }

    /// <summary>Filter options for <see cref="TransferReversal.Customer"/> property.</summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status)
              .Add("reason", Reason)
              .Add("customer", Customer);
    }
}
