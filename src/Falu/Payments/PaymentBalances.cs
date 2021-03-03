using System.Collections.Generic;

namespace Falu.Payments
{
    /// <summary>
    /// Funds that are available to be transferred.
    /// The available balance is categorized by provider and currency.
    /// </summary>
    public class PaymentBalances
    {
        /// <summary>
        /// Breakdown of balance by business code.
        /// The value is represented in the smallest currrency unit.
        /// </summary>
        public Dictionary<string, long> Mpesa { get; set; }
    }
}
