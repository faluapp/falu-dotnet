using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.Messages
{
    /// <summary>Options for filtering and pagination of messages.</summary>
    public record MessagesListOptions : BasicListOptions
    {
        /// <summary>
        /// Range filter options for <see cref="Message.Delivered"/> property.
        /// </summary>
        public RangeFilteringOptions<DateTimeOffset>? Delivered { get; set; }

        /// <summary>
        /// Filter options for <see cref="Message.Status"/> property.
        /// </summary>
        public List<MessageStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList)
                      .AddIfNotNull("delivered", Delivered, ConvertDate);
            return dictionary;
        }
    }
}
