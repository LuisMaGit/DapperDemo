using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using DataManager.DataAccess.Persons;

namespace ConsoleUI.UserInterface.ViewsModels.PersonByName
{
    public class PersonByNameViewModel : SimpleViewModelBehavior<PersonByName>
    {
        private readonly IPersonService _personService;
        private readonly ResponseBehaviors _responseBehaviors;
        
        public PersonByNameViewModel(
            ApplicationUiService appUi,
            SimpleValidationBehavior validationBehavior, 
            PersonByName view,
            IPersonService personService, 
            ResponseBehaviors responseBehaviors) 
            : base(appUi, validationBehavior, view)
        {
            _personService = personService;
            _responseBehaviors = responseBehaviors;
        }

        private string _name;

        protected override bool ValidateInput(string input)
        {
            _name = input;
            return Validators.NameValidator(input);
        }

        protected override async Task RunServiceLogic()
        {
            var response = await _personService.SearchPersonsByName(_name);
            _responseBehaviors.HandleListDataResponse(response);
        }
    }
}