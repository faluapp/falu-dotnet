using System;
using System.Collections.Generic;
using System.Linq;

namespace Falu.Core
{
    /// <summary>
    /// Standard options for filtering and pagination in list operations.
    /// </summary>
    public class BasicListOptions
    {
        /// <summary>
        /// The order to use for sorting the objects returned.
        /// Optional, defaults to <see cref="SortingOrder.Descending"/>.
        /// </summary>
        public SortingOrder? Sorting { get; set; }

        /// <summary>
        /// The maximum number of objects to return.
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// A cursor for use in pagination.
        /// The token from a previous request as gotten from the header of it's response.
        /// For instance, if you make a request and receive 10 objects, the response contain
        /// a <code>X-Continuation-Token</code> header with value <c>bravo</c>, your subsequent
        /// call can include <code>ct=bravo</code>.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Range filter options for <code>created</code> property.
        /// </summary>
        public RangeFilteringOptions<DateTimeOffset>? Created { get; set; }

        /// <summary>
        /// Range filter options for <code>updated</code> property.
        /// </summary>
        public RangeFilteringOptions<DateTimeOffset>? Updated { get; set; }

        internal virtual IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

            dictionary.AddIfNotNull("sort", Sorting, ConvertEnum)
                      .AddIfNotNull("count", Count, ConvertInt32)
                      .AddIfNotNull("ct", Token);

            Created?.PopulateQueryValues("created", dictionary, ConvertDate);
            Updated?.PopulateQueryValues("updated", dictionary, ConvertDate);

            return dictionary;
        }

        internal static string ConvertBool(bool b) => b.ToString().ToLowerInvariant();
        internal static string ConvertDate(DateTimeOffset d) => d.ToString("o");
        internal static string ConvertInt32(int i) => i.ToString();
        internal static string ConvertInt64(long i) => i.ToString();
        internal static string ConvertEnum<T>(T e) where T : Enum => e.GetEnumMemberAttrValueOrDefault();
        internal static string ConvertEnumList<T>(IList<T> list) where T : Enum => string.Join(",", list.Select(l => ConvertEnum(l)));
    }
}
