using Microsoft.Extensions.DependencyInjection;

namespace CG.Sms.Builders
{
    /// <summary>
    /// This class is a default implementation of the <see cref="ISmsStrategyBuilder"/>
    /// </summary>
    internal class SmsStrategyBuilder : ISmsStrategyBuilder
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <inheritdoc/>
        public IServiceCollection Services { get; set; }

        #endregion
    }
}
