using Falu.Core;

namespace Falu.FileLinks;

/// <summary>Options for filtering and pagination of list file links.</summary>
public record FileLinksListOptions : BasicListOptions
{
    /// <summary>Unique identifier of the file.</summary>
    public string? File { get; set; }

    /// <summary>Filter options for <see cref="FileLink.Expired"/> property.</summary>
    public bool? Expired { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("file", File)
              .Add("expired", Expired);
    }
}
