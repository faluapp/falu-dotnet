namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportIPAddress : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// IP address from which the verification was done.
    /// </summary>
    /// <example>::ffff:127.0.0.1</example>
    public string? IP { get; set; }

    /// <summary>The city.</summary>
    /// <example>Nairobi</example>
    public string? City { get; set; }

    /// <summary>
    /// The postal code or zip code.
    /// Each country has its way of denoting postal codes.
    /// </summary>
    /// <example>00100</example>
    public string? PostalCode { get; set; }

    /// <summary>
    /// The state or province.
    /// Also referred to as the <c>province</c>
    /// </summary>
    /// <example>Nairobi</example>
    public string? State { get; set; }

    /// <summary>The country.</summary>
    /// <example>Kenya</example>
    public string? Country { get; set; }

    /// <summary>
    /// Longitudinal measurement (east or west), if available.
    /// </summary>
    /// <example>36.8155</example>
    public double? Longitude { get; set; }

    /// <summary>
    /// Latitudinal measurement (north or south), if available.
    /// </summary>
    /// <example>-1.2841</example>
    public double? Latitude { get; set; }

    /// <summary>
    /// Whether the <c>ip</c> points to a mobile (cellular) connection.
    /// <c>null</c> when the relevant information is unavailable.
    /// </summary>
    public bool? Mobile { get; set; }

    /// <summary>
    /// Whether the <c>ip</c> points to a Web Proxy, Virtual Private Network (VPN), or a TOR exit.
    /// <c>null</c> when the relevant information is unavailable.
    /// </summary>
    public bool? Proxied { get; set; }

    /// <summary>
    /// Whether the <c>ip</c> points to a hosting provider, cloud provider, data center or colocated server farm.
    /// <c>null</c> when the relevant information is unavailable.
    /// </summary>
    public bool? Hosted { get; set; }

    /// <summary>
    /// The <c>ip</c>'s Autonomous System Number (ASN), if available.
    /// </summary>
    public string? Asn { get; set; }
}
