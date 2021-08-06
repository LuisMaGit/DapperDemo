using System.IO;
using ConsoleUI.ServiceLocator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleUI
{
    internal static class Program
    {
        private static void  Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var service = host.Services.CreateScope())
            {
                var app = service.ServiceProvider.GetService<Application>();
                app?.Run();
            }

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices)
                .ConfigureAppConfiguration(ConfigureAppConfiguration);

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddInstallers();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
        }
    }
}