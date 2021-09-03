using Falu.Core;
using System.Collections.Generic;

namespace Falu.PaymentRefunds
{
    /// <summary>Options for filtering and pagination of list payment refunds operation.</summary>
    public record PaymentRefundsListOptions : BasicListOptionsWithMoney
    {
        /// <summary>Filter options for <see cref="PaymentRefund.Status"/> property.</summary>
        public List<PaymentRefundStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList);

            return dictionary;
        }
    }
}
