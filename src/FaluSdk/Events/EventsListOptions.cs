using Falu.Core;

namespace Falu.Events
{
    /// <summary>Options for filtering and pagination of events.</summary>
    public record EventsListOptions : BasicListOptions
    {
        /// <summary>Filter options for <see cref="WebhookEvent{TObject}.Type"/> property.</summary>
        public List<string>? Type { get; set; }

        /// <inheritdoc/>
        internal override void Populate(QueryValues values)
        {
            base.Populate(values);
            values.Add("type", Type);
        }
    }
}
