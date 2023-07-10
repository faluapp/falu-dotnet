using Falu.Core;

namespace Falu.MessageStreams;

/// <summary>Options for filtering and pagination of message streams.</summary>
public record MessageStreamsListOptions : BasicListOptions
{
    /// <summary>Filter options for <see cref="MessageStream.Type"/> property.</summary>
    public List<string>? Type { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("type", Type);
    }
}
