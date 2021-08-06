using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI.ServiceLocator
{
    public interface IServiceInstaller
    {
        public void InstallServices(IServiceCollection service);
    }
}