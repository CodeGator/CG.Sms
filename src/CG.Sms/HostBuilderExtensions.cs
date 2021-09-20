using CG.Validations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IHostBuilder"/>
    /// type.
    /// </summary>
    public static partial class HostBuilderExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds a standard sms service to the specified host 
        /// builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder to use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the service.</param>
        /// <returns>The value of the <paramref name="hostBuilder"/> parameter, 
        /// for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// one or more of the required parameters is missing or invalid.</exception>
        public static IHostBuilder AddSms(
            this IHostBuilder hostBuilder,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(hostBuilder, nameof(hostBuilder));

            // Setup sms.
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                // Get the configuration section from the standard location.
                var section = hostBuilderContext.Configuration.GetSection(
                    "Services:Sms"
                    );

                // Use the section to configure the sms service.
                serviceCollection.AddSms(
                    section,
                    serviceLifetime
                    );
            });

            // Return the builder.
            return hostBuilder;
        }

        #endregion
    }
}
