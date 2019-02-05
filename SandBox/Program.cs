using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Web;

namespace SandBox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
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
                    env.ConfigureNLog($"Settings\\nlog.config");
                })
#if DEBUG
                .UseUrls("http://localhost:1987")
#endif
                .UseNLog();
    }
}
