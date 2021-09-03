using System.Collections.Generic;

namespace Falu.Core
{
    /// <summary>Standard options for filtering and pagination in list operations with money.</summary>
    public record BasicListOptionsWithMoney : BasicListOptions, IHasCurrency
    {
        /// <summary>
        /// Filter options for <see cref="IHasCurrency.Currency"/> property.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Filter options for <code>amount</code> property.
        /// </summary>
        public RangeFilteringOptions<long>? Amount { get; set; }

        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("currency", Currency)
                      .AddIfNotNull("amount", Amount, ConvertInt64);
            return dictionary;
        }
    }
}
