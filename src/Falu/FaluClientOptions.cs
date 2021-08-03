using System.Text.Json;
using System.Text.Json.Serialization;

namespace Falu
{
    /// <summary>
    /// Service configuration options for <see cref="FaluClient"/>
    /// </summary>
    public class FaluClientOptions
    {
        /// <summary>
        /// The ApiVersion that the SDK conforms to.
        /// For internal-use only.
        /// </summary>
        public const string ApiVersion = "2020-09-08";

        /// <summary>
        /// Serialization options.
        /// For internal-use only;
        /// </summary>
        public JsonSerializerOptions SerializerOptions { get; } = CreateSerializerOptions();

        /// <summary>
        /// The API Key for authenticating requests to Falu servers.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Maximum number of retries made by the client.
        /// Defaults to <c>2</c>.
        /// </summary>
        public int Retries { get; set; } = 2;

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
