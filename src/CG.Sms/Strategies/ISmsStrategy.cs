using CG.Business.Strategies;
using CG.Sms.Strategies.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Sms.Strategies
{
    /// <summary>
    /// This interface represents a strategy for sending SMS messages.
    /// </summary>
    public interface ISmsStrategy : IStrategy
    {
        /// <summary>
        /// This method sends a SMS message to a list of phone numbers.
        /// </summary>
        /// <param name="toPhones">The list of phone numbers to use for the 
        /// operation.</param>
        /// <param name="message">The message to use for the operation.</param>
        /// <param name="token">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        Task<IEnumerable<SmsResult>> SendAsync(
            IEnumerable<string> toPhones,
            string message,
            CancellationToken token
            );
    }


    /// <summary>
    /// This interface represents a strategy for sending sms messages.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public interface ISmsStrategy<TOptions> : ISmsStrategy
        where TOptions : SmsStrategyOptions
    {

    }
}
