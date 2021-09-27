using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting; 
using Serilog;
using System; 

namespace MailManager
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                        .AddJsonFile("appsettings.json")
                                        .Build();

            Log.Logger = new LoggerConfiguration()
                                        .ReadFrom
                                        .Configuration(configuration)
                                        .CreateLogger();

            CreateHostBuilder(args).Build().Run();

            try
            {
                Log.Information("Application starting up");
                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Cannot start application");
            }
            finally
            {
                Log.CloseAndFlush();
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