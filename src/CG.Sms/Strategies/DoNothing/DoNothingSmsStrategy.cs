using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Sms.Strategies.DoNothing
{
    /// <summary>
    /// This class is a "do nothing" implementation of <see cref="ISmsStrategy{TOptions}"/>
    /// </summary>
    public class DoNothingSmsStrategy :
        SmsStrategyBase,
        ISmsStrategy
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="DoNothingSmsStrategy"/>
        /// class.
        /// </summary>
        public DoNothingSmsStrategy()
        {

        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method sends a SMS message to a list of phone numbers.
        /// </summary>
        /// <param name="toPhones">The list of phone numbers to use for the 
        /// operation.</param>
        /// <param name="message">The message to use for the operation.</param>
        /// <param name="token">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public override Task<IEnumerable<SmsResult>> SendAsync(
            IEnumerable<string> toPhones,
            string message,
            CancellationToken token
            )
        {
            // Create a dummy result since we doesn't actually send anything.
            var retValue = new SmsResult[]
            {
                new SmsResult() { SmsId = $"{Guid.NewGuid():N}" }
            }.AsEnumerable();

            // Return the result.
            return Task.FromResult(retValue);
        }

        #endregion
    }
}
