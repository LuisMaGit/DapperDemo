using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI.ServiceLocator
{
    public static class ServiceInstallerExtension
    {
        public static void AddInstallers(this IServiceCollection services)
        {
            var classes = Assembly.Load(nameof(ConsoleUI)).ExportedTypes.Where(t =>
                !t.IsInterface && !t.IsAbstract && typeof(IServiceInstaller).IsAssignableFrom(t));
            
            var instances = classes.Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>().ToList();
            
            foreach (var instance in instances)
            {
                instance?.InstallServices(services);
            }
            
        }
    }
}