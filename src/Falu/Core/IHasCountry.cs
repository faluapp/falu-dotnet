namespace Falu.Core
{
    /// <summary>
    /// Interface that identifies objects with a <c>Country</c> property.
    /// </summary>
    public interface IHasCountry
    {
        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-3166-country-codes.html">ISO country code</see>,
        /// in lowercase, where to the record exists.
        /// </summary>
        string? Country { get; set; }
    }
}
