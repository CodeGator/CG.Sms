using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CG.Sms.QuickStart
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(
            IConfiguration configuration
            )
        {
            Configuration = configuration;
        }

        public void ConfigureServices(
            IServiceCollection services
            )
        {
            // I'm in the process of trying to solve an issue where
            //   the SMS service/stratregy registrations disappear
            //   after we add them - hence, the duplicate call here.

            services.AddSms(
                Configuration.GetSection("Services:Sms"),
                ServiceLifetime.Singleton
                );

            services.AddSms(
                Configuration.GetSection("Services:Sms"),
                ServiceLifetime.Singleton
                );
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env
            )
        {
            app.UseSms(
                env,
                "Services:Sms"
                );
        }
    }
}
