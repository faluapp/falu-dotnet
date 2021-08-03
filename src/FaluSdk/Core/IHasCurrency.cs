namespace Falu.Core
{
    /// <summary>
    /// Interface that identifies objects with a <c>Currency</c> property.
    /// </summary>
    public interface IHasCurrency
    {
        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        /// <example>kes</example>
        string? Currency { get; set; }
    }
}
