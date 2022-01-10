using Falu.Core;

namespace Falu.Identity;

/// <summary>Options for filtering and pagination of identity marketing data.</summary>
public record MarketingListOptions : BasicListOptions
{
    /// <inheritdoc/>
    public string Country { get; set; } = "ken";

    /// <summary>
    /// The gender of the entity.
    /// When not specified, any gender is returned.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Range filter options for <code>birthday</code> property but based on age.
    /// Cannot be used with <see cref="Birthday"/>.
    /// </summary>
    public RangeFilteringOptions<int>? Age { get; set; }

    /// <summary>
    /// Range filter options for <code>birthday</code> property.
    /// Cannot be used with <see cref="Age"/>.
    /// </summary>
    public RangeFilteringOptions<DateTimeOffset>? Birthday { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("country", Country)
              .Add("gender", Gender)
              .Add("age", QueryValues.FromRange(Age))
              .Add("birthday", QueryValues.FromRange(Birthday));
    }
}
