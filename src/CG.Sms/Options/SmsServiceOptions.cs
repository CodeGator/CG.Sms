using CG.Business.Services.Options;
using CG.Sms.Strategies.Options;
using System.ComponentModel.DataAnnotations;

namespace CG.Sms.Options
{
    /// <summary>
    /// This class contains options information for the <see cref="SmsService"/>
    /// class.
    /// </summary>
    public class SmsServiceOptions : ServiceOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the strategy for the sms service.
        /// </summary>
        [Required]
        public SmsStrategyOptions Strategy { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmsServiceOptions"/>
        /// class.
        /// </summary>
        public SmsServiceOptions()
        {
            // Set default values here.
            Strategy = new SmsStrategyOptions();
        }

        #endregion
    }
}
