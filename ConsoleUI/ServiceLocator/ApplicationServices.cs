using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Services;
using ConsoleUI.UserInterface.Services.UIProvider;
using ConsoleUI.UserInterface.ViewsModels.Home;
using ConsoleUI.UserInterface.ViewsModels.PersonById;
using ConsoleUI.UserInterface.ViewsModels.PersonByName;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI.ServiceLocator
{
    public class ApplicationServices : IServiceInstaller
    {
        public void InstallServices(IServiceCollection service)
        {
            service.AddTransient<Application>();
            //VIEWS
            service.AddTransient<PersonById>();
            service.AddTransient<PersonByName>();
            //VIEW MODELS
            service.AddTransient<HomeViewModel>();
            service.AddTransient<PersonByIdViewModel>();
            service.AddTransient<PersonByNameViewModel>();
            //SERVICES
            service.AddTransient<IUIProvider, SimpleUIProvider>();
            service.AddTransient<ApplicationUiService>();
            //BEHAVIORS
            service.AddTransient<ResponseBehaviors>();
            service.AddTransient<SimpleValidationBehavior>();
        }
    }
}