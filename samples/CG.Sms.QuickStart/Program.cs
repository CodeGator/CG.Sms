using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace CG.Sms.QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // < -- call our startup class ...
                })
                .Build();

            try
            {
                Console.WriteLine($"Getting sms service ...");
                var sms = host.Services.GetRequiredService<ISmsService>();

                Console.WriteLine($"sending sms ...");
                var results = sms.SendAsync(
                    new[] { "9188675309" },
                    "this is a test sms message"
                    ).Result;
                Console.WriteLine($"sms id: {results.First().SmsId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to exit ...");
                Console.ReadKey();
            }
        }
    }
}
