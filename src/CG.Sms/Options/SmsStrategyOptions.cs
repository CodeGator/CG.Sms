using CG.Business.Strategies.Options;
using System.ComponentModel.DataAnnotations;

namespace CG.Sms.Options
{
    /// <summary>
    /// This class contains options for an sms strategy.
    /// </summary>
    public class SmsStrategyOptions : StrategyOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the strategy name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// This property contains an optional assembly name for strategies
        /// located in an external assembly.
        /// </summary>
        public string Assembly { get; set; }

        #endregion
    }
}
