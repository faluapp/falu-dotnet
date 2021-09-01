using Falu.Core;
using System.Collections.Generic;

namespace Falu.TransferReversals
{
    /// <summary>
    /// Options for filtering and pagination of list transfer reversals operation.
    /// </summary>
    public record TransferReversalsListOptions : BasicListOptionsWithMoney
    {
        /// <summary>
        /// Filter options for <code>status</code> property.
        /// </summary>
        public List<TransferReversalStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList);

            return dictionary;
        }
    }
}
