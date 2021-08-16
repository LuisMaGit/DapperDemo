using System;
using System.Net.Mail;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services;
using DataManager.DataAccess.PersonServices;

namespace ConsoleUI.UserInterface.Views.PersonByEmail
{
    public class PersonByEmailViewModel : SimpleViewModelBehavior<PersonByEmail>
    {
        private readonly IPersonService _personService;
        private string _email;

        public PersonByEmailViewModel(
            ApplicationUiService appUi,
            SimpleValidationBehavior validationBehavior,
            PersonByEmail view,
            IPersonService personService,
            ResponseBehaviors responseBehaviors)
            : base(appUi, validationBehavior, view, responseBehaviors)
        {
            _personService = personService;
        }

        protected override bool ValidateInput(string input)
        {
            return Validators.ValidateEmail(input, out _email);
        }

        protected override async Task RunServiceLogic()
        {
            var response = await _personService.GetPersonByEmailAsync(_email);
            ResponseBehaviors.HandleSingleDataResponse(response);
        }
    }
}