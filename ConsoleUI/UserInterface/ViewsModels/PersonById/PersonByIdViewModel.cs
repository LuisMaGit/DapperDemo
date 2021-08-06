using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using DataManager.DataAccess.Persons;

namespace ConsoleUI.UserInterface.ViewsModels.PersonById
{
    public class PersonByIdViewModel : SimpleViewModelBehavior<PersonById>
    {
        //Services
        private readonly IPersonService _personService;
        private readonly ResponseBehaviors _responseBehaviors;

        //Props
        private int _id;
        
        public PersonByIdViewModel(
            ApplicationUiService appUi,
            ResponseBehaviors behaviors,
            SimpleValidationBehavior validationBehavior,
            PersonById view,
            IPersonService personService)
            : base(appUi, validationBehavior, view)
        {
            _personService = personService;
            _responseBehaviors = behaviors;
        }

        protected override bool ValidateInput(string input)
        {
            return Validators.ValidateInt(input, out _id);
        }

        protected override async Task RunServiceLogic()
        {
            var response = await _personService.GetPersonsByIdAsync(_id);
            _responseBehaviors.HandleSingleDataResponse(response);
        }
    }
}