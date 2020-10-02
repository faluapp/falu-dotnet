using Falu.Core;
using System;

namespace Falu.Templates
{
    /// <summary>
    /// A template for sending messages.
    /// </summary>
    public class Template : TemplatePatchModel, IHasId, IHasCreated, IHasUpdated, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }
    }
}
