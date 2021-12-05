using Falu.Core;

namespace Falu.Files;

/// <summary>Options for filtering and pagination of list files.</summary>
public record FilesListOptions : BasicListOptions
{
    /// <summary>Filter options for <code>purpose</code> property.</summary>
    public List<FilePurpose>? Purpose { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("purpose", Purpose);
    }
}
