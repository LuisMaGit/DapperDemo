using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using ConsoleUI.UserInterface.Views.AllFullPersons;
using ConsoleUI.UserInterface.Views.AllPersons;
using ConsoleUI.UserInterface.Views.FullPersonByAddress;
using ConsoleUI.UserInterface.Views.FullPersonById;
using ConsoleUI.UserInterface.Views.InsertPerson;
using ConsoleUI.UserInterface.Views.PersonByEmail;
using ConsoleUI.UserInterface.Views.PersonById;
using ConsoleUI.UserInterface.Views.PersonByName;

namespace ConsoleUI.UserInterface.Views.Home
{
    public class HomeViewModel
    {
        //SERVICES
        private readonly ApplicationUiService _appUi;
        private readonly PersonByIdViewModel _personByIdViewModel;
        private readonly PersonByEmailViewModel _personByEmailViewModel;
        private readonly PersonByNameViewModel _personByNameViewModel;
        private readonly FullPersonByIdViewModel _fullPersonByIdViewModel;
        private readonly InsertPersonViewModel _insertPersonViewModel;
        private readonly AllPersonsViewModel _allPersonsViewModel;
        private readonly AllFullPersonsViewModel _allFullPersonsViewModel;
        private readonly FullPersonByAddressViewModel _fullPersonByAddressViewModel;

        //PROPS
        private Dictionary<int, ActionBook> _actionsData;
        private readonly List<string> _homeMenuTexts = new List<string>();
        private readonly List<int> _homeMenuIdxs = new List<int>();

        public HomeViewModel(
            ApplicationUiService appUi,
            PersonByIdViewModel personByIdViewModel,
            PersonByNameViewModel personByNameViewModel,
            PersonByEmailViewModel personByEmailViewModel,
            FullPersonByIdViewModel fullPersonByIdViewModel,
            InsertPersonViewModel insertPersonViewModel,
            AllPersonsViewModel allPersonsViewModel,
            AllFullPersonsViewModel allFullPersonsViewModel, 
            FullPersonByAddressViewModel fullPersonByAddressViewModel)
        {
            _appUi = appUi;
            _personByIdViewModel = personByIdViewModel;
            _personByNameViewModel = personByNameViewModel;
            _personByEmailViewModel = personByEmailViewModel;
            _fullPersonByIdViewModel = fullPersonByIdViewModel;
            _insertPersonViewModel = insertPersonViewModel;
            _allPersonsViewModel = allPersonsViewModel;
            _allFullPersonsViewModel = allFullPersonsViewModel;
            _fullPersonByAddressViewModel = fullPersonByAddressViewModel;
            _BindActionsData();
            _ResolvePricipalMenuItems();
        }


        private void _BindActionsData()
        {
            var personByIb = new ActionBook
            {
                Id = 1,
                Name = "Person by id (Simple data read in Person table)",
                Run = _personByIdViewModel.InitViewModel
            };
            var personByName = new ActionBook
            {
                Id = 2,
                Name = "Person by name (Simple data read in Person table)",
                Run = _personByNameViewModel.InitViewModel
            };
            var personByEmail = new ActionBook
            {
                Id = 3,
                Name = "Person by email (Simple data read in Person table)",
                Run = _personByEmailViewModel.InitViewModel
            };
            var fullPersonById = new ActionBook
            {
                Id = 4,
                Name = "Full person by id" +
                       " (Data read with object map in Person/Address tables)",
                Run = _fullPersonByIdViewModel.InitViewModel
            };
            var insertPerson = new ActionBook
            {
                Id = 5,
                Name = "Insert person (Transaction in Person/Address tables)",
                Run = _insertPersonViewModel.InitViewModel
            };
            var allPersons = new ActionBook
            {
                Id = 6,
                Name = "All persons (Pagination in Person table)",
                Run = _allPersonsViewModel.InitViewModel
            };
            var allFullPersonsPaginated = new ActionBook
            {
                Id = 7,
                Name = "All full persons data (Pagination in Person/Addres table)",
                Run = _allFullPersonsViewModel.InitViewModel
            };            
            var fullPersonsByAddressPaginated = new ActionBook
            {
                Id = 8,
                Name = "Search address (Pagination in Address -> Persons table)",
                Run = _fullPersonByAddressViewModel.InitViewModel
            };
            _actionsData = new Dictionary<int, ActionBook>
            {
                {personByIb.Id, personByIb},
                {personByName.Id, personByName},
                {personByEmail.Id, personByEmail},
                {fullPersonById.Id, fullPersonById},
                {insertPerson.Id, insertPerson},
                {allPersons.Id, allPersons},
                {allFullPersonsPaginated.Id, allFullPersonsPaginated},
                {fullPersonsByAddressPaginated.Id, fullPersonsByAddressPaginated},
            };
        }

        private void _ResolvePricipalMenuItems()
        {
            foreach (var action in _actionsData.Values)
            {
                _homeMenuTexts.Add(action.Text);
                _homeMenuIdxs.Add(action.Id);
            }
        }

        //LOGIC
        private string _GetActionSelected()
        {
            _appUi.SelectSomeAction();
            return _appUi.GetInput();
        }

        private async Task _HandleSelectionAsync()
        {
            var input = _GetActionSelected();
            while (!Validators.ValidateMenuSelection(input, _homeMenuIdxs))
            {
                _appUi.InvalidValue();
                _appUi.Menu(_homeMenuTexts);
                input = _GetActionSelected();
            }

            var idAction = int.Parse(input);
            await _actionsData[idAction].Run();
        }

        //EVENTS
        public async Task AskForActionAsync()
        {
            _appUi.Menu(_homeMenuTexts);
            await _HandleSelectionAsync();
        }
    }
}