using Falu.Core;
using System.Collections.Generic;

namespace Falu.Transfers
{
    /// <summary>Options for filtering and pagination of transfers.</summary>
    public record TransfersListOptions : BasicListOptionsWithMoney
    {
        /// <summary>Filter options for <see cref="Transfer.Status"/> property.</summary>
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
