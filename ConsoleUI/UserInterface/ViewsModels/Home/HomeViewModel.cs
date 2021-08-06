using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using ConsoleUI.UserInterface.ViewsModels.PersonById;
using ConsoleUI.UserInterface.ViewsModels.PersonByName;

namespace ConsoleUI.UserInterface.ViewsModels.Home
{
    public class HomeViewModel
    {
        //SERVICES
        private readonly ApplicationUiService _appUi;
        private readonly PersonByIdViewModel _personByIdViewModel;
        private readonly PersonByNameViewModel _personByNameViewModel;

        //PROPS
        private Dictionary<int, ActionBook> _actionsData;
        private readonly List<string> _homeMenuTexts = new List<string>();
        private readonly List<int> _homeMenuIdxs = new List<int>();

        public HomeViewModel(
            ApplicationUiService appUi, 
            PersonByIdViewModel personByIdViewModel, 
            PersonByNameViewModel personByNameViewModel)
        {
            _appUi = appUi;
            _personByIdViewModel = personByIdViewModel;
            _personByNameViewModel = personByNameViewModel;
            _BindActionsData();
            _ResolvePricipalMenuItems();
        }

        private void _BindActionsData()
        {
            var dataGetPersonByIb = new ActionBook
            {
                Id = 1,
                Name = "Get person by id",
                Run = _GetPersonByIdAsync
            };
            var dataGerPersonByName = new ActionBook
            {
                Id = 2,
                Name = "Get person by name",
                Run = _GetPersonByNameAsync,
            };

            _actionsData = new Dictionary<int, ActionBook>
            {
                {dataGetPersonByIb.Id, dataGetPersonByIb},
                {dataGerPersonByName.Id, dataGerPersonByName},
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

        //DATA
        private async Task _GetPersonByIdAsync()
        {
            await _personByIdViewModel.InitViewModel();
        }

        private async Task _GetPersonByNameAsync()
        {
            await _personByNameViewModel.InitViewModel();
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