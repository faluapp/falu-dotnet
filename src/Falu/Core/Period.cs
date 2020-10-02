using System;

namespace Falu.Core
{
    ///
    public class Period
    {
        /// <summary>
        /// The starting date of the period.
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// The ending date of the period
        /// </summary>
        public DateTimeOffset End { get; set; }
    }
}
