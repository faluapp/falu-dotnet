namespace Falu.Identity
{
    /// <summary>
    /// Details about a query for marketing data
    /// </summary>
    public class IdentityMarketingQuery
    {
        /// <inheritdoc/>
        public string Country { get; set; } = "ken";

        /// <summary>
        /// The age restriction to query for.
        /// </summary>
        public IdentityMarketingAgeModel Age { get; set; }

        /// <summary>
        /// The gender of the entity.
        /// When not specified, any gender is returned.
        /// </summary>
        public Gender? Gender { get; set; }
    }
}
