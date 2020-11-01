using CG.Business.Strategies;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Sms.Strategies
{
    /// <summary>
    /// This class is a "do nothing" implementation of the <see cref="ISmsStrategy"/>
    /// interface.
    /// </summary>
    public abstract class SmsStrategyBase : StrategyBase, ISmsStrategy
    {
        /// <inheritdoc />
        public abstract Task<IEnumerable<SmsResult>> SendAsync(
            IEnumerable<string> toPhones,
            string message,
            CancellationToken token
            );
    }
}
