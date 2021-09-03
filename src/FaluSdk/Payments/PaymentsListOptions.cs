using Falu.Core;
using System.Collections.Generic;

namespace Falu.Payments
{
    /// <summary>Options for filtering and pagination of payments.</summary>
    public record PaymentsListOptions : BasicListOptionsWithMoney
    {
        /// <summary>Filter options for <see cref="Payment.Status"/> property.</summary>
        public List<PaymentStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList);

            return dictionary;
        }
    }
}
