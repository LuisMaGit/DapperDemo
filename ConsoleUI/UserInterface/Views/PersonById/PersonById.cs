using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Services;
using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Views.PersonById
{
    public class PersonById : SimpleViewBehavior
    {
        private readonly ApplicationUiService _appUi;

        public PersonById(
            IUIProvider uiProvider,
            ApplicationUiService appUi) : base(uiProvider)
        {
            _appUi = appUi;
        }

        public override void EnterValue()
        {
            _appUi.EnterAnId();
        }
    }
}