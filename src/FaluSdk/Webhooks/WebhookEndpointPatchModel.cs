﻿using Falu.Core;

namespace Falu.Webhooks;

/// <summary>
/// A model representing details that can be changed about a Webhook endpoint
/// </summary>
public class WebhookEndpointPatchModel : IHasDescription, IHasMetadata
{
    /// <summary>
    /// The list of events to enable for this endpoint.
    /// Possible values are available in <see cref="EventTypes"/>.
    /// </summary>
    public List<string>? Events { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <summary>
    /// The status of the webhook.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The URL of the webhook endpoint
    /// </summary>
    public string? Url { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
