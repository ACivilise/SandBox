using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SandBox.DataAccess.DBContext;
using Serilog;
using Serilog.Events;
using System;

namespace SandBox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
          .Enrich.FromLogContext()
          .WriteTo.Console()
          // Add this line:
          .WriteTo.File(
              @"logs/internal-nlog.txt",
              fileSizeLimitBytes: 1_000_000,
              rollOnFileSizeLimit: true,
              shared: true,
              flushToDiskInterval: TimeSpan.FromSeconds(1)).CreateLogger();

            Log.Logger.Information("Lancement de l'application");
            try
            {

                var host =  CreateWebHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    Log.Logger.Information("Lancement des migrations");
                    services.MigrateDb();
                    Log.Logger.Information("Fin des migrations");
                    services.SeedData();
                }
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");

            }
            finally
            {
                Log.CloseAndFlush();

            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>() 
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    // delete all default configuration providers
                    config.Sources.Clear();
                    IHostingEnvironment env = hostContext.HostingEnvironment;
                    config.AddEnvironmentVariables();
                    config.SetBasePath(env.ContentRootPath);
                    //config.AddJsonFile($"Settings\\database.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"Settings\\settings.json", optional: true, reloadOnChange: true);
                })
#if DEBUG
                .UseUrls("http://localhost:1987")
#endif
                // supprime l'information du serveur utilisé dans l'entete HTTP
                .UseKestrel(options => options.AddServerHeader = false)
                .UseSerilog();
    }
}
