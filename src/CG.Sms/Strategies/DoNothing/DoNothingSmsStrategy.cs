using CG.Business.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Sms.Strategies
{
    /// <summary>
    /// This class is a "do nothing" implementation of the <see cref="ISmsStrategy"/>
    /// interface.
    /// </summary>
    internal class DoNothingSmsStrategy : SmsStrategyBase, ISmsStrategy
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc />
        public override Task<IEnumerable<SmsResult>> SendAsync(
            IEnumerable<string> toPhones,
            string message,
            CancellationToken token
            )
        {
            var ret = new SmsResult()
            {
                SmsId = $"{Guid.NewGuid()}"
            };

            return Task.FromResult(new SmsResult[] { ret }.AsEnumerable());
        }

        #endregion
    }
}
