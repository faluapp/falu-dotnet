﻿using Falu.Core;

namespace Falu.Messages;

/// <summary>
/// Information for creating and sending message.
/// </summary>
public class MessageCreateOptions : IHasMetadata
{
    /// <summary>
    /// Destination phone number in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// </summary>
    public string? To { get; set; }

    /// <summary>
    /// Actual message content to be sent.
    /// Required if <see cref="Template"/> is not specified.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// The template to use.
    /// Required if <see cref="Body"/> is not specified.
    /// </summary>
    public MessageCreateOptionsTemplate? Template { get; set; }

    /// <summary>
    /// Media to be sent with the message.
    /// </summary>
    public IList<MessageCreateOptionsMedia>? Media { get; set; }

    /// <summary>
    /// The stream to use.
    /// It can either be the name or unique identifier of the stream.
    /// If not provided, message will default to the "transactional" stream.
    /// </summary>
    public string Stream { get; set; } = "transactional";

    /// <summary>
    /// Schedule for sending the message(s).
    /// When <see langword="null"/>, the message(s)
    /// is/are enqueued for immediate sending.
    /// </summary>
    public MessageCreateOptionsSchedule? Schedule { get; set; }

    /// <summary>
    /// Identifier of the Customer this Message belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
