using Falu.Core;

namespace Falu.TemporaryKeys;

/// <summary>Options for filtering and pagination of temporary keys.</summary>
public record TemporaryKeysListOptions : BasicListOptions
{
    /// <summary>Filter options for <see cref="TemporaryKey.Objects"/> property.</summary>
    public string? Object { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("object", Object);
    }
}
