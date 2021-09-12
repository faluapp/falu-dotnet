﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace Falu.Core
{
    /// <summary>Helper for handling query values.</summary>
    public sealed class QueryValues : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly Dictionary<string, string> values;

        ///
        public QueryValues(Dictionary<string, string>? values = null)
        {
            // keys are case insensitive
            this.values = values == null
                ? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                : new Dictionary<string, string>(values, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>Gets or sets the value associated with the specified key.</summary>
        /// <param name="key">The key of the value to get or set.</param>
        public string this[string key] => values[key];

        ///
        public QueryValues Add(string key, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                values.Add(key, value);
            }

            return this;
        }

        ///
        public QueryValues Remove(string key)
        {
            values.Remove(key);
            return this;
        }

        ///
        public QueryValues Add(string key, object? value)
        {
            if (value is null) return this;

            return value switch
            {
                bool b => Add(key, b.ToString().ToLowerInvariant()),
                DateTimeOffset dto => Add(key, dto.ToString("O")),
                DateTime dt => Add(key, dt.ToString("O")),
                int i => Add(key, i.ToString()),
                long l => Add(key, l.ToString()),
                string s when !string.IsNullOrWhiteSpace(s) => Add(key, s),
                _ => throw new InvalidOperationException($"'{value.GetType().FullName}' objects are not supported"),
            };
        }

        ///
        public QueryValues Add<T>(string key, T? value) where T : struct, Enum => Add(key, value?.GetEnumMemberAttrValueOrDefault());

        ///
        public QueryValues Add<T>(string key, IEnumerable<T>? value) where T : struct, Enum => Add(key, value?.Select(v => v.GetEnumMemberAttrValueOrDefault()));

        ///
        public QueryValues Add(string key, IEnumerable<string>? value) => Add(key, value is null ? null : string.Join(",", value));

        ///
        public QueryValues Add(string property, QueryValues? other)
        {
            if (other is null) return this;

            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentException($"'{nameof(property)}' cannot be null or whitespace.", nameof(property));
            }

            foreach (var kvp in other.values)
            {
                Add($"{property}.{kvp.Key}", kvp.Value);
            }

            return this;
        }

        ///
        internal Dictionary<string, string> ToDictionary() => values;

        private string Generate()
        {
            var first = true;
            var builder = new StringBuilder();
            foreach (var parameter in values)
            {
                if (parameter.Value is null) continue;

                builder.Append(first ? '?' : '&');
                builder.Append(UrlEncoder.Default.Encode(parameter.Key));
                builder.Append('=');
                builder.Append(UrlEncoder.Default.Encode(parameter.Value));
                first = false;
            }

            return builder.ToString();
        }

        /// <inheritdoc/>
        public override string ToString() => Generate();

        ///
        public static QueryValues? FromRange<T>(RangeFilteringOptions<T>? options = null) where T : struct, IComparable<T>, IEquatable<T>
        {
            if (options is not null)
            {
                var opt = options.Value;
                return new QueryValues().Add("lt", opt.LessThan)
                                        .Add("lte", opt.LessThanOrEqualTo)
                                        .Add("gt", opt.GreaterThan)
                                        .Add("gte", opt.GreaterThanOrEqualTo);
            }

            return null;
        }

        #region IEnumerable

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => values.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

    }
}