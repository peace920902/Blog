using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;

namespace Lazcat.BlogApiService
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .MinimumLevel.Information()
                    .CreateLogger();
                Log.Logger.Information("Host Start");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e.ToString());
                return -1;
            }
            finally
            {
                Log.Logger.Information("Host down");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
