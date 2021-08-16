using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using DataManager.DataAccess.PersonServices;

namespace ConsoleUI.UserInterface.Views.FullPersonById
{
    public class FullPersonByIdViewModel : SimpleViewModelBehavior<FullPersonById>
    {
        private readonly IPersonService _personService;

        public FullPersonByIdViewModel(
            ApplicationUiService appUi,
            SimpleValidationBehavior validationBehavior,
            ResponseBehaviors responseBehaviors,
            FullPersonById view, 
            IPersonService personService) :
            base(appUi, validationBehavior, view, responseBehaviors)
        {
            _personService = personService;
        }

        private int _id;

        protected override bool ValidateInput(string input)
        {
            return Validators.ValidateInt(input, out _id);
        }

        protected override async Task RunServiceLogic()
        {
            var response = await _personService.GetFullPersonByIdAsync(_id);
            ResponseBehaviors.HandleSingleDataResponse(response);
        }
    }
}