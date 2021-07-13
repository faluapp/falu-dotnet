using Falu.Core;
using System.Collections.Generic;

namespace Falu.Transfers
{
    /// <summary>
    /// Options for filtering and pagination of list transfer operation.
    /// </summary>
    public class TransfersListOptions : BasicListOptions
    {
        /// <summary>
        /// Filter options for <code>status</code> property.
        /// </summary>
        public List<TransferStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList);

            return dictionary;
        }
    }
}
