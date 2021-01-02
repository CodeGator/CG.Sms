using CG.Configuration;
using CG.Sms;
using CG.Sms.Properties;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds services and strategies for sending sms messages 
        /// to the specified service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="configuration">The configuration to use for the operation.</param>
        /// <returns>A <see cref="IServiceCollection"/> object for building up
        /// an strategies for the service.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IServiceCollection AddSms(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Register the service.
            serviceCollection.AddSingleton<ISmsService, SmsService>();

            // Register the strategy(s).
            serviceCollection.AddStrategies(configuration);

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
