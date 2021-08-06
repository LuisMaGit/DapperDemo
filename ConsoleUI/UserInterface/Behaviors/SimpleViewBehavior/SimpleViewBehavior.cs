using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior
{
    public abstract class SimpleViewBehavior
    {
        protected readonly IUIProvider UiProvider;

        protected SimpleViewBehavior(IUIProvider uiProvider)
        {
            UiProvider = uiProvider;
        }

        public abstract void EnterValue();
    }
}