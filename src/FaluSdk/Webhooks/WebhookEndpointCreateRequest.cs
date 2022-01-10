namespace Falu.Webhooks;

/// <summary>
/// Information for creating a webhook endpoint.
/// </summary>
public class WebhookEndpointCreateRequest : WebhookEndpointPatchModel
{
    /// <summary>
    /// The format to use for webhook requests.
    /// </summary>
    public string? Format { get; set; }
}
