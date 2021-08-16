using DataManager.DataAccess;
using DataManager.DataAccess.PersonServices;
using DataManager.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI.ServiceLocator
{
    public class DataServices : IServiceInstaller
    {
        public void InstallServices(IServiceCollection service)
        {
            service.AddSingleton<ConnectionStringHelper>();
            service.AddTransient<IPersonService, PersonService>();
            service.AddTransient<ISimpleDataAcces, SimpleDataAccess>();
        }
    }
}