using CG.Business.Strategies;
using CG.Sms.Options;
using Microsoft.Extensions.Options;
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


    /// <summary>
    /// This class is a base implementation of <see cref="ISmsStrategy{TOptions}"/>
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class SmsStrategyBase<TOptions> :
        StrategyBase<TOptions>,
        ISmsStrategy<TOptions>
        where TOptions : SmsStrategyOptionsBase, new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmsStrategyBase{TOptions}"/>
        /// class.
        /// </summary>
        protected SmsStrategyBase(
            IOptions<TOptions> options
            ) : base(options)
        {

        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc />
        public abstract Task<IEnumerable<SmsResult>> SendAsync(
            IEnumerable<string> toPhones,
            string message,
            CancellationToken token
            );

        #endregion
    }
}
