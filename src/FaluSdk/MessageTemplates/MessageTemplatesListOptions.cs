using Falu.Core;

namespace Falu.MessageTemplates;

/// <summary>Options for filtering and pagination of message templates.</summary>
public record MessageTemplatesListOptions : BasicListOptions
{
    /// <summary>
    /// Filter options for <c>type</c> property.
    /// </summary>
    public List<string>? Type { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("type", Type);
    }
}
