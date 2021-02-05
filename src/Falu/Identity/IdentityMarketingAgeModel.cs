namespace Falu.Identity
{
    /// <summary>
    /// The age range specified for restricted identity marketing data.
    /// </summary>
    public class IdentityMarketingAgeModel
    {
        /// <summary>
        /// The minimum age of entities to request.
        /// Allowed range 18 to 100.
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// The maximum age of entities to request.
        /// Allowed range 18 to 100.
        /// </summary>
        public int Max { get; set; }
    }
}
