namespace Falu;

/// <summary>
/// Service configuration options for <see cref="FaluClient{TOptions}"/>
/// </summary>
public class FaluClientOptions
{
    /// <summary>The ApiVersion that the SDK conforms to.</summary>
    internal const string ApiVersion = "2024-06-01";

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
    /// Host to use when uploading files
    /// </summary>
    public string FilesHost { get; set; } = "files.falu.io";

    /// <summary>
    /// Information about the application.
    /// It is recommended for use only with third party services for identification purposes.
    /// </summary>
    public ApplicationInformation? Application { get; set; }
}
