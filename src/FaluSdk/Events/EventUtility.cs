using Falu.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Falu.Events
{
    /// <summary>
    /// This class contains utility methods to process event objects in Falu's webhooks.
    /// </summary>
    /// <remarks>
    /// Use only for <see cref="Webhooks.WebhookFormat.Basic"/>.
    /// </remarks>
    public static class EventUtility
    {
        private const int DefaultTimeTolerance = 300;

        /// <summary>
        /// Parses a JSON string from a Falu webhook into a <see cref="WebhookEvent{TObject}"/> object, while
        /// verifying the <a href="https://docs.falu.io/webhooks/signatures">webhook's
        /// signature</a>.
        /// </summary>
        /// <param name="json">The JSON string to parse.</param>
        /// <param name="signature">The value of the <c>X-Falu-Signature</c> header from the webhook request.</param>
        /// <param name="secret">The webhook endpoint's signing secret.</param>
        /// <param name="tolerance">The time tolerance, in seconds. Defaults to 300 seconds.</param>
        /// <param name="utcNow">The timestamp to use for the current time. Defaults to current time.</param>
        /// <returns>The deserialized <see cref="WebhookEvent{TObject}"/>.</returns>
        /// <exception cref="FaluException">
        /// Thrown if the signature verification fails for any reason.
        /// </exception>
        public static WebhookEvent<T>? ConstructEvent<T>(string json,
                                                        string signature,
                                                        string secret,
                                                        long? tolerance = null,
                                                        long? utcNow = null)
        {
            ValidateSignature(json, signature, secret, tolerance, utcNow);
            return ParseEvent<T>(json);
        }

        /// <summary>
        /// Parses a JSON string from a webhook into a <see cref="WebhookEvent{TObject}"/> object.
        /// </summary>
        /// <param name="json">The JSON string to parse.</param>
        /// <returns>The deserialized <see cref="WebhookEvent{TObject}"/>.</returns>
        /// <remarks>
        /// This method doesn't verify <a href="https://docs.falu.io/webhooks/signatures">webhook
        /// signatures</a>. It's recommended that you use
        /// <see cref="ConstructEvent(string, string, string, long?, long?)"/> instead.
        /// </remarks>
        public static WebhookEvent<T>? ParseEvent<T>(string json)
        {
            var options = FaluClientOptions.CreateSerializerOptions();
            return System.Text.Json.JsonSerializer.Deserialize<WebhookEvent<T>>(json, options);
        }

        /// <summary>
        /// Validate a signature provided alongside a webhook event.
        /// </summary>
        /// <param name="json">The raw JSON body payload</param>
        /// <param name="signature">The value of the <see cref="HeadersNames.XFaluSignature"/> header from the webhook request.</param>
        /// <param name="secret">The webhook endpoint's signing secret.</param>
        /// <param name="tolerance">The time tolerance, in seconds. Defaults to 300 seconds</param>
        /// <param name="utcNow">The timestamp to use for the current time. Defaults to current time.</param>
        public static void ValidateSignature(string json, string signature, string secret, long? tolerance = null, long? utcNow = null)
        {
            var actualItems = ParseSignature(signature);
            var expected = ComputeSignature(secret, actualItems["t"].FirstOrDefault(), json);

            if (!IsSignaturePresent(expected, actualItems["sha256"]))
            {
                throw new FaluException($"The signature for the webhook is not present in the {HeadersNames.XFaluSignature} header.");
            }

            var webhookUtc = Convert.ToInt32(actualItems["t"].FirstOrDefault());
            var now = utcNow ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            tolerance ??= DefaultTimeTolerance;

            if (Math.Abs(now - webhookUtc) > tolerance)
            {
                throw new FaluException("The webhook cannot be processed because the current timestamp is outside of the allowed tolerance.");
            }
        }

        private static ILookup<string, string> ParseSignature(string signature)
        {
            return signature.Trim()
                            .Split(',')
                            .Select(item => item.Trim().Split(new[] { '=' }, 2))
                            .ToLookup(item => item[0], item => item[1]);
        }

        private static bool IsSignaturePresent(string signature, IEnumerable<string> signatures)
        {
            return signatures.Any(key => string.Equals(key, signature, StringComparison.Ordinal));
        }

        private static string ComputeSignature(string secret, string timestamp, string payload)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var payloadBytes = Encoding.UTF8.GetBytes($"{timestamp}.{payload}");

            using var hasher = new HMACSHA256(secretBytes);
            var hash = hasher.ComputeHash(payloadBytes);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}
