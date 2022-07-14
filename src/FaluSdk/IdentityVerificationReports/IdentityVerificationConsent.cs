namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationConsent
{
    /// <summary>
    /// The timestamp marking when the user gave consent for the identity verification to be done.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// The IP address from which the user gave consent for the identity verification to be done.
    /// </summary>
    /// <example>::ffff:127.0.0.1</example>
    public string? IP { get; set; }

    /// <summary>
    /// The user agent of the browser from which the user gave consent for the identity verification to be done.
    /// </summary>
    /// <example>Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36</example>
    public string? UserAgent { get; set; }
}
