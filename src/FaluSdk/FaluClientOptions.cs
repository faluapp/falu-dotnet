using System.Text.Json;
using System.Text.Json.Serialization;

namespace Falu;

/// <summary>
/// Service configuration options for <see cref="FaluClient{TOptions}"/>
/// </summary>
public class FaluClientOptions
{
    private static JsonSerializerOptions? serializerOptions;

    /// <summary>The ApiVersion that the SDK conforms to.</summary>
    internal const string ApiVersion = "2022-09-01";

    /// <summary>Serialization options.</summary>
    internal JsonSerializerOptions SerializerOptions { get; } = GetSerializerOptions();

    /// <summary>
    /// The API Key for authenticating requests to Falu servers.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Maximum number of retries to be made by the client, in addition to the original call.
    /// Defaults to <c>2</c>.
    /// </summary>
    public int Retries { get; set; } = 2;

    /// <summary>
    /// Information about the application.
    /// It is recommended for use only with thirdy party plugins/services for identification purposes.
    /// </summary>
    public ApplicationInformation? Application { get; set; }

    internal static JsonSerializerOptions GetSerializerOptions()
    {
        serializerOptions ??= new(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };

        return serializerOptions;
    }
}
