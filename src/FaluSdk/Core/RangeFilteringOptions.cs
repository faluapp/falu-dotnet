namespace Falu.Core;

/// <summary>Standard options for range filtering.</summary>
/// <typeparam name="T"></typeparam>
/// <param name="lt">Option for less than (&lt;).</param>
/// <param name="lte">Option for less than or equal to (&lt;=).</param>
/// <param name="gt">Option for greater than (&gt;).</param>
/// <param name="gte">Option for greater than or equal to (&gt;=).</param>
public readonly struct RangeFilteringOptions<T>(T? lt = null, T? lte = null, T? gt = null, T? gte = null) where T : struct, IComparable<T>, IEquatable<T>
{
    /// <summary>Option for less than (&lt;).</summary>
    public T? LessThan { get; init; } = lt;

    /// <summary>Option for less than or equal to (&lt;=).</summary>
    public T? LessThanOrEqualTo { get; init; } = lte;

    /// <summary>Option for greater than (&gt;).</summary>
    public T? GreaterThan { get; init; } = gt;

    /// <summary>Option for greater than or equal to (&gt;=).</summary>
    public T? GreaterThanOrEqualTo { get; init; } = gte;
}
