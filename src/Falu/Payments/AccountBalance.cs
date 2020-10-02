using System.Collections.Generic;

namespace Falu.Payments
{
    /// <summary>
    /// Funds that are available to be transferred.
    /// The available balance is categorized by provider and currency.
    /// </summary>
    public class AccountBalance
    {
        /// <summary>
        /// Breakdown of balance by business code.
        /// </summary>
        public Dictionary<string, float> Mpesa { get; set; }
    }
}
