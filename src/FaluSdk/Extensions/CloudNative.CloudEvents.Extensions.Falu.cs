using CloudNative.CloudEvents.Core;

namespace CloudNative.CloudEvents.Extensions;

/// <summary>
/// Support for the <see href="https://github.com/faluapp/cloudevents/blob/main/extensions/falu.md">falu</see>
/// CloudEvent extension.
/// </summary>
public static class Falu
{
    /// <summary>
    /// <see cref="CloudEventAttribute"/> representing the 'workspace' extension attribute.
    /// </summary>
    public static CloudEventAttribute WorkspaceAttribute { get; } =
        CloudEventAttribute.CreateExtension("workspace", CloudEventAttributeType.String);

    /// <summary>
    /// <see cref="CloudEventAttribute"/> representing the 'live' extension attribute.
    /// </summary>
    public static CloudEventAttribute LiveModeAttribute { get; } =
        CloudEventAttribute.CreateExtension("live", CloudEventAttributeType.Boolean);

    /// <summary>
    /// A read-only sequence of all attributes related to the Falu extension.
    /// </summary>
    public static IEnumerable<CloudEventAttribute> AllAttributes { get; } =
        new[] { WorkspaceAttribute, LiveModeAttribute }.ToList().AsReadOnly();


    /// <summary>
    /// Retrieves the <see cref="WorkspaceAttribute"/> value from the event, without any
    /// further transformation.
    /// </summary>
    /// <param name="cloudEvent">The CloudEvent from which to retrieve the attribute. Must not be null.</param>
    /// <returns>The <see cref="WorkspaceAttribute"/> value, as a string, or null if the attribute is not set.</returns>
    public static string? GetWorkspace(this CloudEvent cloudEvent) =>
        (string?)Validation.CheckNotNull(cloudEvent, nameof(cloudEvent))[WorkspaceAttribute];

    /// <summary>
    /// Retrieves the <see cref="LiveModeAttribute"/> value from the event, without any
    /// further transformation.
    /// </summary>
    /// <param name="cloudEvent">The CloudEvent from which to retrieve the attribute. Must not be null.</param>
    /// <returns>The <see cref="LiveModeAttribute"/> value, as a string, or null if the attribute is not set.</returns>
    public static bool? GetLiveMode(this CloudEvent cloudEvent) =>
        (bool?)Validation.CheckNotNull(cloudEvent, nameof(cloudEvent))[LiveModeAttribute];
}
