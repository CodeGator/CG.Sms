using CG.Reflection;
using CG.Sms;
using CG.Sms.Builders;
using CG.Sms.Options;
using CG.Sms.Properties;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

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
        /// <remarks>
        /// <para>
        /// This method builds up an sms service using information read from 
        /// the specified configuration. 
        /// </para>
        /// <para>
        /// Here is an example for the JSON configuration:
        /// <code language="json">
        /// {
        ///     "Sms": {
        ///       "Strategy": {
        ///           "Name": "Twilio",
        ///           "Assembly": "CG.Sms.Twilio"
        ///         },
        ///       "Twilio": {
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
        /// The <c>Sms</c> section is an example of a strategy section. This will vary, of
        /// course, depending on which strategy is named in the <c>Strategy</c> node. In this
        /// case, we see a made up example for a Twilio based SMS strategy. 
        /// </para>
        /// </remarks>
        public static IServiceCollection AddSms(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Configure the sms service options.
            serviceCollection.ConfigureOptions<SmsServiceOptions>(
                configuration,
                out var smsServiceOptions
                );

            // Register the service.
            serviceCollection.AddSingleton<ISmsService, SmsService>();

            // Should we load an assembly for the strategy?
            if (!string.IsNullOrEmpty(smsServiceOptions.Strategy.Assembly))
            {
                // Load the assembly for the strategy. 
                _ = Assembly.Load(
                    smsServiceOptions.Strategy.Assembly
                    );
            }

            var methodName = "";

            // Watch for a missing or empty strategy name.
            if (string.IsNullOrEmpty(smsServiceOptions.Strategy.Name))
            {
                // So we have an extension method to call.
                methodName = $"AddDoNothingStrategy";
            }
            else
            {
                // Format the name of the extension method.
                methodName = $"Add{smsServiceOptions.Strategy.Name}Strategy";
            }

            // Look for specified extension method.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(ISmsStrategyBuilder),
                methodName,
                new Type[] { typeof(IConfiguration) }
                );

            // Did we fail to find anything?
            if (false == methods.Any())
            {
                // Panic!
                throw new MissingMethodException(
                    message: string.Format(
                        Resources.ServiceCollectionExtensions_MethodNotFound,
                        methodName
                        )
                    );
            }

            // Create the strategy builder.
            var strategyBuilder = new SmsStrategyBuilder()
            {
                Services = serviceCollection
            };

            // We'll use the first matching method.
            var method = methods.First();

            // Invoke the extension method.
            method.Invoke(
                null,
                new object[] { strategyBuilder, configuration }
                );

            // Return the service collection.
            return serviceCollection;
        }

        // *******************************************************************

        /// <summary>
        /// This method adds services and strategies for sending sms messages 
        /// to the specified service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="builderAction">The delegate to use for the operation.</param>
        /// <returns>A <see cref="IServiceCollection"/> object for building up
        /// an strategies for the service.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IServiceCollection AddSms(
            this IServiceCollection serviceCollection,
            Action<ISmsStrategyBuilder> builderAction
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(builderAction, nameof(builderAction));

            // Create the strategy builder.
            var strategyBuilder = new SmsStrategyBuilder()
            {
                Services = serviceCollection
            };

            // Register the service.
            serviceCollection.AddSingleton<ISmsService, SmsService>();

            // Allow the caller to customize the builder.
            builderAction(strategyBuilder);

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
