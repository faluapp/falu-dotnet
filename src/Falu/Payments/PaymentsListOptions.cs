using Falu.Core;
using System.Collections.Generic;

namespace Falu.Payments
{
    /// <summary>
    /// Options for filtering and pagination of list payments operation.
    /// </summary>
    public class PaymentsListOptions : BasicListOptions
    {
        /// <summary>
        /// Filter options for <code>status</code> property.
        /// </summary>
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
