using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.PaginationViewBehavior;
using DataManager.DataAccess.PersonServices;
using DataManager.Domain;
using DataManager.Domain.Data.Person;

namespace ConsoleUI.UserInterface.Views.AllPersons
{
    public class AllPersonsViewModel : IViewModelInitializer
    {
        private readonly IPersonService _personService;
        private readonly PaginationViewModelBehavior _paginationBehavior;


        public AllPersonsViewModel(
            IPersonService personService,
            PaginationViewModelBehavior paginationBehavior)
        {
            _personService = personService;
            _paginationBehavior = paginationBehavior;
        }


        private async Task<DataResponseModel<List<Person>>> GetPersonsPaginatedAsync()
        {
            return await _personService.GetPersonsPaginatedAsync(_paginationBehavior.CurrentPage);
        }

        public async Task InitViewModel()
        {
            await _paginationBehavior.RunPaginationWith(GetPersonsPaginatedAsync);
        }
    }
}