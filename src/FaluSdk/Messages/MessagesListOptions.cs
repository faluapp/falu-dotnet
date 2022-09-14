using Falu.Core;

namespace Falu.Messages;

/// <summary>Options for filtering and pagination of messages.</summary>
public record MessagesListOptions : BasicListOptions
{
    /// <summary>
    /// Unique identifier of the message stream to filter for.
    /// </summary>
    public string? Stream { get; set; }

    /// <summary>
    /// Unique identifier of the message batch to filter for.
    /// </summary>
    public string? Batch { get; set; }

    /// <summary>
    /// Range filter options for <see cref="Message.Delivered"/> property.
    /// </summary>
    public RangeFilteringOptions<DateTimeOffset>? Delivered { get; set; }

    /// <summary>
    /// Range filter options for <see cref="Message.Sent"/> property.
    /// </summary>
    public RangeFilteringOptions<DateTimeOffset>? Sent { get; set; }

    /// <summary>
    /// Filter options for <see cref="Message.Status"/> property.
    /// </summary>
    public List<string>? Status { get; set; }

    /// <summary>
    /// Filter options for <see cref="Message.To"/> property.
    /// </summary>
    public string? To { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("stream", Stream)
              .Add("batch", Batch)
              .Add("status", Status)
              .Add("delivered", QueryValues.FromRange(Delivered))
              .Add("sent", QueryValues.FromRange(Sent))
              .Add("to", To);
    }
}
