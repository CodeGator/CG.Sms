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
        /// <param name="configuration">the configuration to use for the operation.</param>
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

            // Check the configuration path, just in case we need to adjust it, to 
            //   work with this method.

            // Get the path for the current section.
            var path = configuration.GetPath();

            // Were we called with the configuration root?
            if (true == string.IsNullOrEmpty(path))
            {
                // Point to the proper configuration section.
                configuration = configuration.GetSection("Services:Sms");

                // Get the new path.
                path = configuration.GetPath();
            }
            else if (false == path.EndsWith("Sms"))
            {
                // Point to the proper configuration section.
                configuration = configuration.GetSection("Sms");

                // Get the new path.
                path = configuration.GetPath();
            }

            // Now we should be pointed to the SMS section, but, just in case, 
            //   let's do one more check. We're doing this here because configuration
            //   bugs are difficult and frustrating to troubleshoot, so, we want to 
            //   provide as much feedback as is practical to the caller.

            if (false == path.EndsWith("Sms"))
            {
                // Panic!
                throw new ConfigurationException(
                    message: string.Format(
                        Resources.NotSmsSection,
                        nameof(AddSms),
                        path
                        )
                    );
            }

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
