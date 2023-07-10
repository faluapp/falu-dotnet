using Falu.Core;

namespace Falu.Files;

/// <summary>Options for filtering and pagination of list files.</summary>
public record FilesListOptions : BasicListOptions
{
    /// <summary>Filter options for <see cref="File.Purpose"/> property.</summary>
    public List<string>? Purpose { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("purpose", Purpose);
    }
}
