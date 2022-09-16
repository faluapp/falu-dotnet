namespace Falu.EvaluationReports;

///
public class EvaluationReportConsent
{
    /// <summary>
    /// Time at which the user gave consent.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// IP address from which the user gave consent.
    /// </summary>
    public string? IP { get; set; }

    /// <summary>
    /// User agent of the device (e.g. browser) from which the user gave consent.
    /// </summary>
    public string? UserAgent { get; set; }
}
