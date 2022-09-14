using Microsoft.Extensions.Primitives;
using System.Collections;
using System.Text;
using System.Text.Encodings.Web;

namespace Falu.Core;

/// <summary>Helper for handling query values.</summary>
public sealed class QueryValues : ICollection<KeyValuePair<string, StringValues>>
{
    private readonly Dictionary<string, StringValues> values;

    ///
    public QueryValues(Dictionary<string, StringValues>? values = null)
    {
        // keys are case insensitive
        this.values = values == null
            ? new Dictionary<string, StringValues>(StringComparer.OrdinalIgnoreCase)
            : new Dictionary<string, StringValues>(values, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>Gets or sets the value associated with the specified key.</summary>
    /// <param name="key">The key of the value to get or set.</param>
    public StringValues this[string key] => values[key];

    ///
    public QueryValues Add(string key, StringValues? value)
    {
        if (value is not null)
        {
            values.Add(key, value.Value);
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
    public QueryValues Add(string key, IEnumerable<string>? value) => Add(key, (StringValues?)value);

    ///
    public QueryValues Add(string key, IList<string>? value) => Add(key, value?.ToArray());

    ///
    public QueryValues Add(string key, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var v = new[] { value, };
            if (values.TryGetValue(key, out var value2))
            {
                values.Remove(key);
                v = value2.Concat(v).ToArray();
            }
            values.Add(key, v);
        }

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
            string s => Add(key, s),
            _ => throw new InvalidOperationException($"'{value.GetType().FullName}' objects are not supported"),
        };
    }

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
            Add($"{property}.{kvp.Key}", (StringValues?)kvp.Value);
        }

        return this;
    }

    ///
    internal Dictionary<string, StringValues> ToDictionary() => values;

    private string Generate()
    {
        var first = true;
        var builder = new StringBuilder();
        foreach (var parameter in values)
        {
            if (parameter.Value == default(StringValues)) continue;

            // for each item add a query parameter value, the server does not understand when combined
            foreach (var value in parameter.Value)
            {
                if (value is null) continue;

                builder.Append(first ? '?' : '&');
                builder.Append(UrlEncoder.Default.Encode(parameter.Key));
                builder.Append('=');
                builder.Append(UrlEncoder.Default.Encode(value));
                first = false;
            }
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
    public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() => values.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    #region ICollection

    /// <inheritdoc/>
    public int Count => values.Count;

    /// <inheritdoc/>
    bool ICollection<KeyValuePair<string, StringValues>>.IsReadOnly => false;

    /// <inheritdoc/>
    void ICollection<KeyValuePair<string, StringValues>>.Add(KeyValuePair<string, StringValues> item)
        => ((ICollection<KeyValuePair<string, StringValues>>)values).Add(item);

    /// <inheritdoc/>
    public void Clear() => values.Clear();

    /// <inheritdoc/>
    public bool Contains(KeyValuePair<string, StringValues> item) => values.Contains(item);

    /// <inheritdoc/>
    void ICollection<KeyValuePair<string, StringValues>>.CopyTo(KeyValuePair<string, StringValues>[] array, int arrayIndex)
        => ((ICollection<KeyValuePair<string, StringValues>>)values).CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    bool ICollection<KeyValuePair<string, StringValues>>.Remove(KeyValuePair<string, StringValues> item)
        => ((ICollection<KeyValuePair<string, StringValues>>)values).Remove(item);

    #endregion

}
