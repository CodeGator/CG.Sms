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
            services.AddSms(
                Configuration.GetSection("Sms"),
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
                Configuration.GetSection("Sms")
                );
        }
    }
}
