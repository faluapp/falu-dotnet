using Falu.Core;

namespace Falu.Payments;

/// <summary>
/// Funds that are available to be transferred.
/// They are categorized by provider and currency.
/// </summary>
public class MoneyBalances : IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Breakdown of balance by business code.
    /// The value is represented in the smallest currency unit.
    /// </summary>
    public Dictionary<string, long>? Mpesa { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
