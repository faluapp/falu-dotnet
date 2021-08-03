using Falu.Core;
using System;

namespace Falu.Messages
{
    /// <summary>
    /// A message record.
    /// </summary>
    public class Message : MessagePatchModel, IHasId, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string? Id { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Status of the message.
        /// </summary>
        public MessageStatus Status { get; set; }

        /// <summary>
        /// Destination phone number in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
        /// </summary>
        /// <example>+254722000000</example>
        public string? To { get; set; }

        /// <summary>
        /// Gets or sets the contents of the message.
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// Template used for the message.
        /// </summary>
        public MessageSourceTemplate? Template { get; set; }

        /// <summary>
        /// Stream used for the message.
        /// </summary>
        public string? StreamId { get; set; }

        /// <summary>
        /// Provider used for the message.
        /// </summary>
        public MessageProvider? Provider { get; set; }

        /// <summary>
        /// Time at which the message was delivered.
        /// This is dependent on the underlying provider.
        /// </summary>
        public DateTimeOffset? Delivered { get; set; }

        /// <inheritdoc/>
        public string? WorkspaceId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string? Etag { get; set; }
    }
}
