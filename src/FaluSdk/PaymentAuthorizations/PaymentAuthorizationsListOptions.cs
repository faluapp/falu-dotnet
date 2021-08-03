using Falu.Core;
using System.Collections.Generic;

namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Options for filtering and pagination of list payment authorizations operation.
    /// </summary>
    public class PaymentAuthorizationsListOptions : BasicListOptionsWithMoney
    {
        /// <summary>
        /// Filter options for <code>status</code> property.
        /// </summary>
        public List<PaymentAuthorizationStatus>? Status { get; set; }

        /// <summary>
        /// Filter options for <code>authorized</code> property.
        /// </summary>
        public bool? Authorized { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList);
            dictionary.AddIfNotNull("authorized", Authorized, ConvertBool);

            return dictionary;
        }
    }
}
