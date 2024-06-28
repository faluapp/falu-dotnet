namespace Falu.Core;

/// <summary>Standard options for filtering and pagination in list operations with money.</summary>
public record BasicListOptionsWithMoney : BasicListOptions, IHasCurrency
{
    /// <summary>
    /// Filter options for <see cref="IHasCurrency.Currency"/> property.
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Filter options for <code>amount</code> property.
    /// </summary>
    public RangeFilteringOptions<long>? Amount { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("currency", Currency)
              .Add("amount", QueryValues.FromRange(Amount));
    }
}
