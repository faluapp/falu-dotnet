﻿using Falu.Core;
using System.Security.Cryptography;
using System.Text;

namespace Falu.Webhooks;

/// <summary>
/// This class contains utility methods to process incoming webhooks from Falu servers.
/// </summary>
public static class WebhookUtility
{
    private static readonly TimeSpan DefaultTimeTolerance = TimeSpan.FromSeconds(300);

    /// <summary>Validate a signature provided alongside a webhook.</summary>
    /// <param name="payload">The body payload.</param>
    /// <param name="signature">The value of the <see cref="HeadersNames.XFaluSignature"/> header from the webhook request.</param>
    /// <param name="secret">The webhook endpoint's signing secret.</param>
    /// <param name="tolerance">The time tolerance. Defaults to 300 seconds.</param>
    /// <param name="now">The value to use for the current time. Defaults to current time.</param>
    public static void ValidateSignature(string payload, string signature, string secret, TimeSpan? tolerance = null, DateTimeOffset? now = null)
        => ValidateSignature(Encoding.UTF8.GetBytes(payload), signature, secret, tolerance, now);

    /// <summary>Validate a signature provided alongside a webhook.</summary>
    /// <param name="payload">The body payload.</param>
    /// <param name="signature">The value of the <see cref="HeadersNames.XFaluSignature"/> header from the webhook request.</param>
    /// <param name="secret">The webhook endpoint's signing secret.</param>
    /// <param name="tolerance">The time tolerance. Defaults to 300 seconds.</param>
    /// <param name="now">The value to use for the current time. Defaults to current time.</param>
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

        // ensure the timestamp is not in the future, and is within the allowed tolerance
        if (timestamp > now || ((now - timestamp) > tolerance))
        {
            throw new FaluException("The webhook cannot be processed because the current timestamp is outside of the allowed tolerance.");
        }
    }

    private static ILookup<string, string> ParseSignature(string signature)
    {
        static KeyValuePair<string, string> ParseItem(string item)
        {
            var parts = item.Trim().Split(['='], 2);
            if (parts.Length != 2) throw new FaluException("The signature header format is unexpected.");
            return new(parts[0], parts[1]);
        }

        return signature.Trim()
                        .Split(',')
                        .Select(ParseItem)
                        .ToLookup(item => item.Key, item => item.Value);
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
