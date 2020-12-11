using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Basch.Api.Core.Logging;
using Basch.Api.Core.Extensions;

namespace Basch.Api
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args)
                .Build()
                .RegisterDefaultJson();

            builder.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));

                    // Only add debugging and console while debugging the application
                    if (DebugSettings.IsDebugging)
                    {
                        logging.AddDebug();
                        logging.AddConsole();
                    }
                })
                .UseStartup<Startup>();
    }
}
