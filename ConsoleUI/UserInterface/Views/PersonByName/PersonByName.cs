using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Views.PersonByName
{
    public class PersonByName : SimpleViewBehavior
    {
        public PersonByName(IUIProvider uiProvider) : base(uiProvider)
        {
        }

        public override void EnterValue()
        {
            UiProvider.WriteProvider("Enter a person name/last name: ");
        }
    }
}