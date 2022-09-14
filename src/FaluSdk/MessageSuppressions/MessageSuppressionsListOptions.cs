using Falu.Core;

namespace Falu.MessageSuppressions;

/// <summary>Options for filtering and pagination of message suppressions.</summary>
public record MessageSuppressionsListOptions : BasicListOptions
{
    /// <summary>
    /// Unique identifier of the message stream to filter for.
    /// </summary>
    public string? Stream { get; set; }

    /// <summary>
    /// Filter options for <see cref="MessageSuppression.Origin"/> property.
    /// </summary>
    public List<string>? Origin { get; set; }

    /// <summary>
    /// Filter options for <see cref="MessageSuppression.Reason"/> property.
    /// </summary>
    public List<string>? Reason { get; set; }

    /// <summary>
    /// Filter options for <see cref="MessageSuppression.To"/> property.
    /// </summary>
    public string? To { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("stream", Stream)
              .Add("origin", Origin)
              .Add("reason", Reason)
              .Add("to", To);
    }
}
