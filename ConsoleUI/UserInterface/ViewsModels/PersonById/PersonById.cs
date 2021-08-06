using ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior;
using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.ViewsModels.PersonById
{
    public class PersonById : SimpleViewBehavior
    {
        public PersonById(IUIProvider uiProvider) : base(uiProvider) {}

        public override void EnterValue()
        {
            UiProvider.WriteProvider("Enter Id: ");
        }
    }
}