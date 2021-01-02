using CG.Business.Services;
using CG.Sms.Properties;
using CG.Sms.Strategies;
using CG.Validations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Sms
{
    /// <summary>
    /// This class is a default implementation of the <see cref="ISmsService"/>
    /// interface.
    /// </summary>
    public class SmsService : ServiceBase, ISmsService
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a reference to an SMS strategy.
        /// </summary>
        private ISmsStrategy SmsStrategy { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmsService"/>
        /// class.
        /// </summary>
        /// <param name="smsStrategy">A reference to an SMS strategy.</param>
        public SmsService(
            ISmsStrategy smsStrategy
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(smsStrategy, nameof(smsStrategy));

            // Save the reference.
            SmsStrategy = smsStrategy;
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
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public virtual async Task<IEnumerable<SmsResult>> SendAsync(
            IEnumerable<string> toPhones,
            string message,
            CancellationToken token = default
            )
        {
            // Validate the parameters before attempting to us them.
            Guard.Instance().ThrowIfNull(toPhones, nameof(toPhones))
                .ThrowIfNullOrEmpty(message, nameof(message));

            try
            {
                // Defer to the strategy.
                var retValue = await SmsStrategy.SendAsync(
                    toPhones,
                    message,
                    token
                    ).ConfigureAwait(false);

                // Return the results.
                return retValue;
            }
            catch (Exception ex)
            {
                // Wrap the exception.
                throw new SmsServiceException(
                    message: string.Format(
                        Resources.SmsService_SendAsync,
                        nameof(SmsService)
                        ),
                    innerException: ex
                    );
            }
        }

        #endregion

        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method is called when the object is disposed.
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources; false
        /// otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            // Should we cleanup maanged resources?
            if (disposing)
            {
                // Cleanup the strategy.
                SmsStrategy?.Dispose();
            }

            // Give the base class a chance.
            base.Dispose(disposing);
        }

        #endregion
    }
}
