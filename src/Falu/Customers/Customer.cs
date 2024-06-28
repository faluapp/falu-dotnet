using Falu.Core;

namespace Falu.Customers;

///
public class Customer : IHasId, IHasCreated, IHasUpdated, IHasDescription, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// The customer’s primary address.
    /// </summary>
    public PhysicalAddress? Address { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <summary>
    /// The customer’s email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The customer’s full name or business name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The customer’s phone number.
    /// </summary>
    public string? Phone { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
