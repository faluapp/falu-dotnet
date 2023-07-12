namespace Falu.MessageStreams;

/// <summary>
/// Information for creating a message stream.
/// </summary>
public class MessageStreamCreateRequest : MessageStreamPatchModel
{
    /// <summary>
    /// A string used for easier identification.
    /// This value cannot be changed after creation.
    /// Allowed characters are numbers, lowercase ASCII letters, and ‘-’, ‘_’ characters,
    /// and the string has to start with a letter.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Type of stream.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Provider to be used.
    /// </summary>
    public string? Provider { get; set; }
}
