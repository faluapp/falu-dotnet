using System.Text.Json.Serialization;

namespace Falu.Events;

/// <summary>
/// Represents details about a request that triggered a webhook event.
/// Usually embedded in the webhook event.
/// </summary>
public class WebhookEventRequest
{
    /// <summary>
    /// ID of the API request that caused the event. If null, the event was automatic
    /// (e.g., automatic balance updates).
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The idempotency key transmitted during the request, if any.
    /// </summary>
    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }
}
