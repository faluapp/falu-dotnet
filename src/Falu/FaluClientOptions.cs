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
        /// Serialization options.
        /// For internal-use only;
        /// </summary>
        public JsonSerializerOptions SerializerOptions { get; } = CreateSerializerOptions();

        /// <summary>
        /// The API Key for authenticating requests to Falu servers.
        /// </summary>
        public string ApiKey { get; set; }

        internal static JsonSerializerOptions CreateSerializerOptions()
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            serializerOptions.Converters.Add(new JsonStringEnumConverter(serializerOptions?.PropertyNamingPolicy));

            return serializerOptions;
        }
    }
}
