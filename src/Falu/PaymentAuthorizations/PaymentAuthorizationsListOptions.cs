using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Options for filtering and pagination of list payment authorizations operation.
    /// </summary>
    public class PaymentAuthorizationsListOptions : BasicListOptions
    {
        /// <summary>
        /// Range filter options for <code>delivered</code> property.
        /// </summary>
        public RangeFilteringOptions<DateTimeOffset>? Delivered { get; set; } // TODO: fix this

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            Delivered?.PopulateQueryValues("delivered", dictionary, ConvertDate);
            return dictionary;
        }
    }
}
