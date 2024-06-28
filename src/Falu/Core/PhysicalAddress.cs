namespace Falu.Core;

/// <summary>Represents a physical address.</summary>
public class PhysicalAddress
{
    /// <summary>
    /// The first line.
    /// Also referred to as the <c>street-address</c>.
    /// </summary>
    public string? Line1 { get; set; }

    /// <summary>
    /// The second line.
    /// Also referred to as the <c>apt, building, suite no. etc.</c>
    /// </summary>
    public string? Line2 { get; set; }

    /// <summary>
    /// The city.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// The postal code or zip code.
    /// Each country has its way of denoting postal codes.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// The state or province.
    /// Also referred to as the <c>province</c>
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// The country.
    /// </summary>
    public string? Country { get; set; }
}
