using Falu.Core;
using System.Collections.Generic;

namespace Falu.PaymentAuthorizations
{
    /// <summary>Options for filtering and pagination of payment authorizations.</summary>
    public record PaymentAuthorizationsListOptions : BasicListOptionsWithMoney
    {
        /// <summary>Filter options for <see cref="PaymentAuthorization.Status"/> property.</summary>
        public List<PaymentAuthorizationStatus>? Status { get; set; }

        /// <summary>Filter options for <see cref="PaymentAuthorization.Approved"/> property.</summary>
        public bool? Approved { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList);
            dictionary.AddIfNotNull("approved", Approved, ConvertBool);

            return dictionary;
        }
    }
}
