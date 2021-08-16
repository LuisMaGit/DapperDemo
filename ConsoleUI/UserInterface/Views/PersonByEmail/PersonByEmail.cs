using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Views.PersonByEmail
{
    public class PersonByEmail : SimpleViewBehavior
    {
        public PersonByEmail(IUIProvider uiProvider) : base(uiProvider)
        {
        }

        public override void EnterValue()
        {
            UiProvider.WriteLineProvider("Enter a valid email");
        }
    }
}