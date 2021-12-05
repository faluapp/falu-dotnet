using Falu.Core;

namespace Falu.Identity;

/// <summary>
/// Information for searching for an entity's identity.
/// </summary>
public class IdentitySearchModel : IHasCountry
{
    /// <inheritdoc />
    public string? Country { get; set; } = "ken";

    /// <summary>
    /// The phone number to search in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// Required if <see cref="DocumentNumber"/> is not specified.
    /// </summary>
    /// <example>+254722000000</example>
    public string? Phone { get; set; }

    /// <summary>
    /// The kind of document to search for.
    /// Required if <see cref="Phone"/> is not specified.
    /// </summary>
    public IdentityDocumentKind? DocumentType { get; set; }

    /// <summary>
    /// The identification document number to search.
    /// Required if <see cref="Phone"/> is not specified.
    /// </summary>
    public string? DocumentNumber { get; set; }
}
