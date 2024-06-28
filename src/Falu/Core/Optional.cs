namespace Falu.Core;

// Mostly copied from Roslyn
// https://github.com/dotnet/roslyn/blob/5e84fbf00739306fe7122655085740877c989c01/src/Compilers/Core/Portable/Optional.cs

/// <summary>
/// Combines a value, <see cref="Value"/>, and a flag, <see cref="HasValue"/>, 
/// indicating whether or not that value is meaningful.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public readonly struct Optional<T>(T? value)
{
    private readonly bool hasValue = true;

    /// <summary>
    /// Returns <see langword="true"/> if the <see cref="Value"/> will return a meaningful value.
    /// </summary>
    /// <returns></returns>
    public bool HasValue => hasValue;

    /// <summary>
    /// Gets the value of the current object.
    /// Not meaningful unless <see cref="HasValue"/> returns <see langword="true"/>.
    /// </summary>
    /// <remarks>
    /// <para>Unlike <see cref="Nullable{T}.Value"/>, this property does not throw an exception when
    /// <see cref="HasValue"/> is <see langword="false"/>.</para>
    /// </remarks>
    /// <returns>
    /// <para>The value if <see cref="HasValue"/> is <see langword="true"/>; otherwise, the default value for type
    /// <typeparamref name="T"/>.</para>
    /// </returns>
    public T? Value => value;

    /// <summary>
    /// Creates a new object initialized to a meaningful value. 
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator Optional<T>(T value) => new(value);

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    public override string ToString() => hasValue ? value?.ToString() ?? "null" : "unspecified"; // Note: For nullable types, it's possible to have _hasValue true and _value null.
}
