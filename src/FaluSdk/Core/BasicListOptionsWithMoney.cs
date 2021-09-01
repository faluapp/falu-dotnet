using System.Collections.Generic;

namespace Falu.Core
{
    /// <summary>
    /// Standard options for filtering and pagination in list operations with money.
    /// </summary>
    public record BasicListOptionsWithMoney : BasicListOptions, IHasCurrency
    {
        /// <inheritdoc/>
        public string? Currency { get; set; }

        /// <summary>
        /// Filter options for <code>amount</code> property.
        /// </summary>
        public RangeFilteringOptions<long>? Amount { get; set; }

        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("currency", Currency);
            Amount?.PopulateQueryValues("amount", dictionary, ConvertInt64);

            return dictionary;
        }
    }
}
