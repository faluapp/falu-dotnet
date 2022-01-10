using Falu.Core;

namespace Falu.Identity;

/// <summary>
/// Restricted version of <see cref="IdentityRecord"/> for marketing purposes.
/// </summary>
public class MarketingResult : IHasId, IHasCreated, IHasUpdated, IHasCountry, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <inheritdoc/>
    public string? Country { get; set; }

    /// <summary>
    /// The date of birth of the entity if specified.
    /// This value is not guaranteed to be availed.
    /// For entities that are not human (e.g. companies, businesses, NGOs,
    /// this value represents the date of registration or incorporation.
    /// </summary>
    public DateTimeOffset? Birthday { get; set; }

    /// <summary>
    /// Phone numbers attached to the identity.
    /// </summary>
    public List<string>? Phones { get; set; }

    /// <summary>
    /// Email addresses attached to the identity.
    /// </summary>
    public List<string>? Emails { get; set; }

    /// <summary>
    /// The gender of the entity.
    /// This value may be fixed or predicted.
    /// Check <code>GenderConfidence</code> for more information.
    /// </summary>
    public string? Gender { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
