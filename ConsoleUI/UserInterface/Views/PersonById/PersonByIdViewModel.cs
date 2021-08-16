using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using DataManager.DataAccess.PersonServices;

namespace ConsoleUI.UserInterface.Views.PersonById
{
    public class PersonByIdViewModel : SimpleViewModelBehavior<PersonById>
    {
        //Services
        private readonly IPersonService _personService;
        //Props
        private int _id;
        
        public PersonByIdViewModel(
            ApplicationUiService appUi,
            ResponseBehaviors responseBehaviors,
            SimpleValidationBehavior validationBehavior,
            PersonById view,
            IPersonService personService)
            : base(appUi, validationBehavior, view, responseBehaviors)
        {
            _personService = personService;
        }

        protected override bool ValidateInput(string input)
        {
            return Validators.ValidateInt(input, out _id);
        }

        protected override async Task RunServiceLogic()
        {
            var response = await _personService.GetPersonsByIdAsync(_id);
            ResponseBehaviors.HandleSingleDataResponse(response);
        }
    }
}