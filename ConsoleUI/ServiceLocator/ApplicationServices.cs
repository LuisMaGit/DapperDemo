using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.PaginationViewBehavior;
using ConsoleUI.UserInterface.Services;
using ConsoleUI.UserInterface.Services.UIProvider;
using ConsoleUI.UserInterface.Views.AllFullPersons;
using ConsoleUI.UserInterface.Views.AllPersons;
using ConsoleUI.UserInterface.Views.FullPersonByAddress;
using ConsoleUI.UserInterface.Views.FullPersonById;
using ConsoleUI.UserInterface.Views.Home;
using ConsoleUI.UserInterface.Views.InsertPerson;
using ConsoleUI.UserInterface.Views.PersonByEmail;
using ConsoleUI.UserInterface.Views.PersonById;
using ConsoleUI.UserInterface.Views.PersonByName;
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
            service.AddTransient<PersonByEmail>();
            service.AddTransient<FullPersonById>();
            service.AddTransient<InsertPerson>();
            service.AddTransient<PaginationView>();
            service.AddTransient<FullPersonAddress>();
            //VIEW MODELS
            service.AddTransient<HomeViewModel>();
            service.AddTransient<PersonByIdViewModel>();
            service.AddTransient<PersonByNameViewModel>();
            service.AddTransient<PersonByEmailViewModel>();
            service.AddTransient<FullPersonByIdViewModel>();
            service.AddTransient<InsertPersonViewModel>();
            service.AddTransient<AllPersonsViewModel>();
            service.AddTransient<AllFullPersonsViewModel>();
            service.AddTransient<FullPersonByAddressViewModel>();
            //SERVICES
            service.AddTransient<IUIProvider, SimpleUIProvider>();
            service.AddTransient<ApplicationUiService>();
            //BEHAVIORS
            service.AddTransient<ResponseBehaviors>();
            service.AddTransient<PaginationViewModelBehavior>();
            service.AddTransient<SimpleValidationBehavior>();
        }
    }
}