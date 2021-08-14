using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.Payments
{
    /// <summary>
    /// Funds that are available to be transferred.
    /// They are categorized by provider and currency.
    /// </summary>
    public class MoneyBalances : IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Breakdown of balance by business code.
        /// The value is represented in the smallest currrency unit.
        /// </summary>
        public Dictionary<string, long>? Mpesa { get; set; }

        /// <inheritdoc/>
        public string? WorkspaceId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string? Etag { get; set; }
    }
}
