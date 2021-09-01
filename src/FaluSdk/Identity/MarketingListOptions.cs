using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.Identity
{
    /// <summary>
    /// Options for filtering and pagination of list identity marketing data operation.
    /// </summary>
    public record MarketingListOptions : BasicListOptions
    {
        /// <inheritdoc/>
        public string Country { get; set; } = "ken";

        /// <summary>
        /// The gender of the entity.
        /// When not specified, any gender is returned.
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Range filter options for <code>birthday</code> property but based on age.
        /// Cannot be used with <see cref="Birthday"/>.
        /// </summary>
        public RangeFilteringOptions<int>? Age { get; set; }

        /// <summary>
        /// Range filter options for <code>birthday</code> property.
        /// Cannot be used with <see cref="Age"/>.
        /// </summary>
        public RangeFilteringOptions<DateTimeOffset>? Birthday { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("country", Country);
            dictionary.AddIfNotNull("gender", Gender, ConvertEnum);
            Age?.PopulateQueryValues("age", dictionary, ConvertInt32);
            Birthday?.PopulateQueryValues("birthday", dictionary, ConvertDate);
            return dictionary;
        }
    }
}
