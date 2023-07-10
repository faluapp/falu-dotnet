using Falu.Core;

namespace Falu.MessageBatches;

/// <summary>Options for filtering and pagination of message batches.</summary>
public record MessageBatchesListOptions : BasicListOptions
{
    /// <summary>
    /// Unique identifier of the message stream to filter for.
    /// </summary>
    public string? Stream { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("stream", Stream);
    }
}
