using Falu.Core;

namespace Falu.Customers;

///
public class CustomerCreateOptions : IHasDescription, IHasMetadata
{
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
}
