using CG.Business.Strategies;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Sms.Builders
{
    /// <summary>
    /// This interface represents a builder for SMS strategies.
    /// </summary>
    public interface ISmsStrategyBuilder 
    {
        /// <summary>
        /// This property contains a collection of registered services.
        /// </summary>
        IServiceCollection Services { get; set; }
    }
}
