using Falu.Core;
using System.Security.Cryptography;
using System.Text;

namespace Falu.Events;

/// <summary>
/// This class contains utility methods to process event objects in Falu's webhooks.
/// </summary>
public static class EventUtility
{
    private static readonly TimeSpan DefaultTimeTolerance = TimeSpan.FromSeconds(300);

    /// <summary>Validate a signature provided alongside a webhook event.</summary>
    /// <param name="payload">The body payload.</param>
    /// <param name="signature">The value of the <see cref="HeadersNames.XFaluSignature"/> header from the webhook request.</param>
    /// <param name="secret">The webhook endpoint's signing secret.</param>
    /// <param name="tolerance">The time tolerance. Defaults to 300 seconds.</param>
    /// <param name="now">The timestamp to use for the current time. Defaults to current time.</param>
    public static void ValidateSignature(string payload, string signature, string secret, TimeSpan? tolerance = null, DateTimeOffset? now = null)
        => ValidateSignature(Encoding.UTF8.GetBytes(payload), signature, secret, tolerance, now);

    /// <summary>Validate a signature provided alongside a webhook event.</summary>
    /// <param name="payload">The body payload.</param>
    /// <param name="signature">The value of the <see cref="HeadersNames.XFaluSignature"/> header from the webhook request.</param>
    /// <param name="secret">The webhook endpoint's signing secret.</param>
    /// <param name="tolerance">The time tolerance. Defaults to 300 seconds.</param>
    /// <param name="now">The timestamp to use for the current time. Defaults to current time.</param>
    /// <remarks>
    /// Use this to validate the signature in your request pipeline.
    /// </remarks>
    public static void ValidateSignature(byte[] payload, string signature, string secret, TimeSpan? tolerance = null, DateTimeOffset? now = null)
    {
        var actual = ParseSignature(signature);
        var actualTimestamp = actual["t"].FirstOrDefault();
        var expected = ComputeSignature(secret, actualTimestamp, payload);

        if (!IsSignaturePresent(expected, actual["sha256"]))
        {
            throw new FaluException($"The signature for the webhook is not present in the {HeadersNames.XFaluSignature} header.");
        }

        var timestamp = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(actualTimestamp));
        now ??= DateTimeOffset.UtcNow;
        tolerance ??= DefaultTimeTolerance;

        if ((now - timestamp) > tolerance)
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
        => signatures.Any(key => string.Equals(key, signature, StringComparison.Ordinal));

    private static string ComputeSignature(string secret, string? timestamp, byte[] payload)
    {
        var secretBytes = Encoding.UTF8.GetBytes(secret);
        var payloadBytes = Encoding.UTF8.GetBytes($"{timestamp}.").Concat(payload).ToArray();

        using var hasher = new HMACSHA256(secretBytes);
        var hash = hasher.ComputeHash(payloadBytes);
        return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
    }
}
