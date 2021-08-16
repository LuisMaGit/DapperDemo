using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Views.FullPersonByAddress
{
    public class FullPersonAddress
    {
        private readonly IUIProvider _uiProvider;

        public FullPersonAddress(IUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
        }

        public void EnterSearch()
        {
            _uiProvider.WriteLineProvider("Enter an address: ");
        }
    }
}