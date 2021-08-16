using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.PaginationViewBehavior;
using ConsoleUI.UserInterface.Helpers;
using DataManager.DataAccess.PersonServices;
using DataManager.Domain;
using DataManager.Domain.Data.Person;

namespace ConsoleUI.UserInterface.Views.FullPersonByAddress
{
    public class FullPersonByAddressViewModel : IViewModelInitializer
    {
        private readonly PaginationViewModelBehavior _paginationBehavior;
        private readonly IPersonService _personService;
        private readonly SimpleValidationBehavior _validationBehavior;
        private readonly FullPersonAddress _view;
        private string _search;

        public FullPersonByAddressViewModel(
            IPersonService personService,
            PaginationViewModelBehavior paginationBehavior,
            SimpleValidationBehavior validationBehavior,
            FullPersonAddress view)
        {
            _personService = personService;
            _paginationBehavior = paginationBehavior;
            _validationBehavior = validationBehavior;
            _view = view;
        }

        private Task<DataResponseModel<List<FullPerson>>> _SearchAddressFullPersonPaginated()
        {
            return _personService.SearchAddressFullPersonPaginatedAsync(_paginationBehavior.CurrentPage, _search);
        }

        public async Task InitViewModel()
        {
            _search = _validationBehavior.ValidationLogic(_view.EnterSearch, Validators.ValidateAddress);
            await _paginationBehavior.RunPaginationWith(_SearchAddressFullPersonPaginated);
        }
    }
}