using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.Webhooks;

/// <summary>
/// A model representing details that can be changed about a Webhook endpoint
/// </summary>
public class WebhookEndpointUpdateOptions : IHasOptionalDescription, IHasOptionalMetadata
{
    private Optional<string?>? url;
    private Optional<string?>? status;
    private Optional<string?>? description;
    private Optional<List<string>?>? events;
    private Optional<Dictionary<string, string>?>? metadata;

    /// <summary>
    /// The list of events to enable for this endpoint.
    /// Possible values are available in <see cref="EventTypes"/>.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<List<string>?>))]
    public Optional<List<string>?>? Events { get => events; set => events = new(value); }

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Description { get => description; set => description = new(value); }

    /// <summary>
    /// The status of the webhook.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Status { get => status; set => status = new(value); }

    /// <summary>
    /// The URL of the webhook endpoint
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Url { get => url; set => url = new(value); }

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, string>?>))]
    public Optional<Dictionary<string, string>?>? Metadata { get => metadata; set => metadata = new(value); }
}
