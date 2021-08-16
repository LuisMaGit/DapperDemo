using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using DataManager.DataAccess.PersonServices;

namespace ConsoleUI.UserInterface.Views.PersonByName
{
    public class PersonByNameViewModel : SimpleViewModelBehavior<PersonByName>
    {
        private readonly IPersonService _personService;

        public PersonByNameViewModel(
            ApplicationUiService appUi,
            SimpleValidationBehavior validationBehavior,
            PersonByName view,
            IPersonService personService,
            ResponseBehaviors responseBehaviors)
            : base(appUi, validationBehavior, view, responseBehaviors)
        {
            _personService = personService;
        }

        private string _name;

        protected override bool ValidateInput(string input)
        {
            _name = input;
            return Validators.ValidateName(input);
        }

        protected override async Task RunServiceLogic()
        {
            var response = await _personService.SearchPersonsByNameAsync(_name);
            ResponseBehaviors.HandleListDataResponse(response);
        }
    }
}