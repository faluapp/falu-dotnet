using System;
using System.Collections.Generic;

namespace Falu.Core
{
    /// <summary>
    /// Standard options for range filtering.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct RangeFilteringOptions<T> where T : struct, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lt">Option for less than (&lt;).</param>
        /// <param name="lte">Option for less than or equal to (&lt;=).</param>
        /// <param name="gt">Option for greater than (&gt;).</param>
        /// <param name="gte">Option for greater than or equal to (&gt;=).</param>
        public RangeFilteringOptions(T? lt = null, T? lte = null, T? gt = null, T? gte = null)
        {
            LessThan = lt;
            LessThanOrEqualTo = lte;
            GreaterThan = gt;
            GreaterThanOrEqualTo = gte;
        }

        /// <summary>
        /// Option for less than (&lt;).
        /// </summary>
        public T? LessThan { get; init; }

        /// <summary>
        /// Option for less than or equal to (&lt;=).
        /// </summary>
        public T? LessThanOrEqualTo { get; init; }

        /// <summary>
        /// Option for greater than (&gt;).
        /// </summary>
        public T? GreaterThan { get; init; }

        /// <summary>
        /// Option for greater than or equal to (&gt;=).
        /// </summary>
        public T? GreaterThanOrEqualTo { get; init; }

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
