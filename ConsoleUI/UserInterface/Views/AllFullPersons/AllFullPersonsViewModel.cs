using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.PaginationViewBehavior;
using DataManager.DataAccess.PersonServices;
using DataManager.Domain;
using DataManager.Domain.Data.Person;

namespace ConsoleUI.UserInterface.Views.AllFullPersons
{
    public class AllFullPersonsViewModel : IViewModelInitializer
    {
        private readonly PaginationViewModelBehavior _paginationBehavior;
        private readonly IPersonService _personService;

        public AllFullPersonsViewModel(
            PaginationViewModelBehavior paginationBehavior,
            IPersonService personService)
        {
            _paginationBehavior = paginationBehavior;
            _personService = personService;
        }

        private async Task<DataResponseModel<List<FullPerson>>> _GetFullPersonPaginated()
        {
            return await _personService.GetFullPersonPaginatedAsync(_paginationBehavior.CurrentPage);
        }

        public async Task InitViewModel()
        {
            await _paginationBehavior.RunPaginationWith(_GetFullPersonPaginated);
        }
    }
}