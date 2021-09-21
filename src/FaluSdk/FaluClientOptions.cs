using System.Text.Json;
using System.Text.Json.Serialization;

namespace Falu
{
    /// <summary>
    /// Service configuration options for <see cref="FaluClient{TOptions}"/>
    /// </summary>
    public class FaluClientOptions
    {
        /// <summary>
        /// The ApiVersion that the SDK conforms to.
        /// For internal-use only.
        /// </summary>
        public const string ApiVersion = "2021-09-03";

        /// <summary>
        /// Serialization options.
        /// For internal-use only;
        /// </summary>
        internal JsonSerializerOptions SerializerOptions { get; } = CreateSerializerOptions();

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

        internal static JsonSerializerOptions CreateSerializerOptions()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            serializerOptions.Converters.Add(new JsonStringEnumConverter(serializerOptions.PropertyNamingPolicy));

            return serializerOptions;
        }
    }
}
