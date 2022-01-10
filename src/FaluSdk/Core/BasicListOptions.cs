namespace Falu.Core;

/// <summary>
/// Standard options for filtering and pagination in list operations.
/// </summary>
public record BasicListOptions
{
    /// <summary>The order to use for sorting the objects returned.</summary>
    public string? Sorting { get; set; }

    /// <summary>The maximum number of objects to return.</summary>
    public int? Count { get; set; }

    /// <summary>
    /// A cursor for use in pagination.
    /// The token from a previous request as gotten from the header of it's response.
    /// For instance, if you make a request and receive 10 objects, the response contain
    /// a <code>X-Continuation-Token</code> header with value <c>bravo</c>, your subsequent
    /// call can include <code>ct=bravo</code>.
    /// </summary>
    public string? ContinuationToken { get; set; }

    /// <summary>Range filter options for <see cref="IHasCreated.Created"/> property.</summary>
    public RangeFilteringOptions<DateTimeOffset>? Created { get; set; }

    /// <summary>Range filter options for <see cref="IHasUpdated.Updated"/> property.</summary>
    public RangeFilteringOptions<DateTimeOffset>? Updated { get; set; }

    internal virtual void Populate(QueryValues values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        values.Add("sort", Sorting)
              .Add("count", Count)
              .Add("ct", ContinuationToken)
              .Add("created", QueryValues.FromRange(Created))
              .Add("updated", QueryValues.FromRange(Updated));
    }
}
