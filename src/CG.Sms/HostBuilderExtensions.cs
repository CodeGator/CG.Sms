using CG.Sms;
using CG.Validations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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
        /// <returns>The value of the <paramref name="hostBuilder"/> parameter, 
        /// for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// one or more of the required parameters is missing or invalid.</exception>
        /// <remarks>
        /// <para>
        /// The idea with this method, is to create a quick and easy way to setup sms 
        /// services for a hosted application by following a simple configuration convention. 
        /// That convention assumes a configuration section exists like this:
        /// <code language="json">
        /// {
        ///    "Services" : {
        ///       "Sms": {
        ///          "Strategy" : "Twilio",
        ///          "Assembly" : "CG.Sms.Twilio",
        ///          "Twilio": {
        ///          }
        ///       }
        ///    }
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Let's break it down. The <c>Services</c> section is where ALL services for the 
        /// application should be configured. If you're adding a CodeGator service to your
        /// application then you'll want to add that service to this section in order to 
        /// configure the service at startup.
        /// </para>
        /// <para>
        /// Under <c>Services</c>, the <c>Sms</c> section is where the CodeGator sms service 
        /// should be configured. This section contains at least two nodes: <c>Strategy</c> and 
        /// <c>Assembly</c>. The <c>Strategy</c> node tells the host what strategy to load
        /// for the sms service, and as such, is required. The <c>Assembly</c> section is 
        /// optional, and is only needed when the strategy is located in an external assembly 
        /// that should be dynamically loaded at startup.
        /// </para>
        /// <para>
        /// The <c>Smtp</c> section is an example of a strategy section. This will vary, of
        /// course, depending on which strategy is named in the <c>Strategy</c> node. In this
        /// case, we see a made up example for a Twilio based SMS strategy. 
        /// </para>
        /// </remarks>
        public static IHostBuilder AddSms(
            this IHostBuilder hostBuilder
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

                // Does the section have anything in it?
                if (section.GetChildren().Any())
                {
                    // If we get here then we can setup an sms service using 
                    //   whatever happens to be in the configuration section.

                    // Use the section to configure the sms service.
                    serviceCollection.AddSms(section);
                }
                else
                {
                    // If we get here then nothing was configured in the standard
                    //   location for an sms service. Now, unlike services such 
                    //   as logging, we can't define "defaults" here, for sms messages,
                    //   that will always work. So, we'll have to settle for 
                    //   registering an sms service with a "do nothing" strategy,
                    //   so that we don't force everything to constantly check 
                    //   whether an sms service exists, or not.

                    // Setup a standard sms placeholder.
                    serviceCollection.AddSms(options =>
                    {
                        options.AddDoNothingStrategy(section);
                    });
                }
            });

            // Return the builder.
            return hostBuilder;
        }

        #endregion
    }
}
