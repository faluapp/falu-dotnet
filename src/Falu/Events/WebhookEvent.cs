using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.Events
{
    /// <summary>
    /// The basic implementation of a Webhook Event irrespective of the usage
    /// </summary>
    public class WebhookEvent<TObject> : IHasId, IHasCreated, IHasMetadata, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Type of event (e.g. payment.updated, balance.updated, etc.).
        /// Possible values are available in <see cref="Webhooks.EventTypes"/>.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Information on the API request that instigated the event.
        /// </summary>
        public WebhookEventRequest Request { get; set; }

        /// <summary>
        /// Object containing data associated with the event.
        /// </summary>
        public WebhookEventData<TObject> Data { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string> Metadata { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }

    /// <summary>
    /// The basic implementation of a Webhook Event irrespective of the usage
    /// </summary>
    public class WebhookEvent : WebhookEvent<object> { }
}
