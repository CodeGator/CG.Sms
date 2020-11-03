using CG.Collections;
using CG.Validations;
using System;
using System.Collections.Generic;

namespace CG.Sms
{
    /// <summary>
    /// This class contains extensions methods related to the <see cref="ISmsService"/>
    /// type.
    /// </summary>
    public static partial class SmsServiceExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method sends a SMS message to a list of phone numbers.
        /// </summary>
        /// <param name="smsService">The sms service to use for the operation.</param>
        /// <param name="toPhones">The list of phone numbers to use for the 
        /// operation.</param>
        /// <param name="message">The message to use for the operation.</param>
        /// <returns>The results of the operation.</returns>
        /// <exception cref="SmsServiceException">This error is thrown whenever
        /// a problem is encountered while sending an SMS message.</exception>
        /// <exception cref="ArgumentException">This error is thrown whenever one
        /// or more paramters are invalid or missing.</exception>
        public static IEnumerable<SmsResult> Send(
            this ISmsService smsService,
            IEnumerable<string> toPhones,
            string message
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(smsService, nameof(smsService))
                .ThrowIfNull(toPhones, nameof(toPhones))
                .ThrowIfEmpty(toPhones, nameof(toPhones));

            // Send the message.
            var results = smsService.SendAsync(
                toPhones,
                message
                ).Result;

            // Return the results.
            return results;
        }

        // *******************************************************************

        /// <summary>
        /// This method sends a SMS message to a list of phone numbers.
        /// </summary>
        /// <param name="smsService">The sms service to use for the operation.</param>
        /// <param name="toPhones">The list of phone numbers to use for the 
        /// operation.</param>
        /// <param name="message">The message to use for the operation.</param>
        /// <returns>The results of the operation.</returns>
        /// <exception cref="SmsServiceException">This error is thrown whenever
        /// a problem is encountered while sending an SMS message.</exception>
        /// <exception cref="ArgumentException">This error is thrown whenever one
        /// or more paramters are invalid or missing.</exception>
        public static IEnumerable<SmsResult> Send(
            this ISmsService smsService,
            string toPhones,
            string message
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(smsService, nameof(smsService))
                .ThrowIfNull(toPhones, nameof(toPhones));

            // Split the list up
            var phoneList = toPhones.Split(',');

            // Send the message.
            var results = smsService.SendAsync(
                phoneList,
                message
                ).Result;

            // Return the results.
            return results;
        }

        #endregion
    }
}
