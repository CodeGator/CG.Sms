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
            Host.CreateDefaultBuilder()
                .AddSms()
                .Build()
                .RunDelegate(host =>
                {
                    try
                    {
                        Console.WriteLine($"fetching sms service ...");
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
                    
                });            
        }
    }
}
