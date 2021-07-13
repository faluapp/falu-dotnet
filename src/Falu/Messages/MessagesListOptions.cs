using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.Messages
{
    /// <summary>
    /// Options for filtering and pagination of list messages operation.
    /// </summary>
    public class MessagesListOptions : BasicListOptions
    {
        /// <summary>
        /// Range filter options for <code>delivered</code> property.
        /// </summary>
        public RangeFilteringOptions<DateTimeOffset>? Delivered { get; set; }

        /// <summary>
        /// Filter options for <code>status</code> property.
        /// </summary>
        public List<MessageStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);
            Delivered?.PopulateQueryValues("delivered", dictionary, ConvertDate);
            dictionary.AddIfNotNull("status", Status, ConvertEnumList);
            return dictionary;
        }
    }
}
