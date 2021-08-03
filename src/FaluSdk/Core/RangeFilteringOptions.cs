using System;
using System.Collections.Generic;

namespace Falu.Core
{
    /// <summary>
    /// Standard options for range filtering.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RangeFilteringOptions<T> where T : struct, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Option for less than (&lt;).
        /// </summary>
        public T? LessThan { get; set; }

        /// <summary>
        /// Option for less than or equal to (&lt;=).
        /// </summary>
        public T? LessThanOrEqualTo { get; set; }

        /// <summary>
        /// Option for greater than (&gt;).
        /// </summary>
        public T? GreaterThan { get; set; }

        /// <summary>
        /// Option for greater than or equal to (&gt;=).
        /// </summary>
        public T? GreaterThanOrEqualTo { get; set; }

        internal IDictionary<string, string> PopulateQueryValues(string property, IDictionary<string, string> dictionary, Func<T, string> converter)
        {
            if (string.IsNullOrEmpty(property)) throw new ArgumentException($"'{nameof(property)}' cannot be null or empty", nameof(property));
            if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
            if (converter is null) throw new ArgumentNullException(nameof(converter));

            dictionary.AddIfNotNull(property, "lt", LessThan, converter)
                      .AddIfNotNull(property, "lte", LessThanOrEqualTo, converter)
                      .AddIfNotNull(property, "gt", GreaterThan, converter)
                      .AddIfNotNull(property, "gte", GreaterThanOrEqualTo, converter);

            return dictionary;
        }
    }
}
