using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;

namespace Falu.TransferReversals
{
    /// <summary>Options for filtering and pagination of transfer reversals.</summary>
    public record TransferReversalsListOptions : BasicListOptionsWithMoney
    {
        /// <summary>Filter options for <see cref="TransferReversal.Status"/> property.</summary>
        public List<TransferReversalStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override void Populate(QueryValues values)
        {
            base.Populate(values);
            values.Add("status", Status);
        }
    }
}
