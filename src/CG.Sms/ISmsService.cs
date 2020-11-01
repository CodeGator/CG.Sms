using CG.Business.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Sms
{
    /// <summary>
    /// This interface represents an object that sends emails.
    /// </summary>
    public interface ISmsService : IService
    {
        /// <summary>
        /// This method sends a SMS message to a list of phone numbers.
        /// </summary>
        /// <param name="toPhones">The list of phone numbers to use for the 
        /// operation.</param>
        /// <param name="message">The message to use for the operation.</param>
        /// <param name="token">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        /// <exception cref="SmsServiceException">This error is thrown whenever
        /// a problem is encountered while sending an SMS message.</exception>
        /// <exception cref="ArgumentException">This error is thrown whenever one
        /// or more paramters are invalid or missing.</exception>
        Task<IEnumerable<SmsResult>> SendAsync(
            IEnumerable<string> toPhones,
            string message,
            CancellationToken token = default
            );
    }
}
