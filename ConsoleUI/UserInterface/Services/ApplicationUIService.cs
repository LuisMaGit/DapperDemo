using System.Collections.Generic;
using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Services
{
    public class ApplicationUiService
    {
        private readonly IUIProvider _uiProvider;

        public ApplicationUiService(IUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
        }

        public void Menu(IEnumerable<string> actions)
        {
            foreach (var action in actions)
            {
                _uiProvider.WriteLineProvider(action);
            }
        }

        public void SelectSomeAction()
        {
            _uiProvider.WriteProvider("Select some action: ");
        }

        public void InvalidValue()
        {
            _uiProvider.WriteLineProvider("These value is incorrect");
        }

        public void Loading()
        {
            _uiProvider.WriteLineProvider("Loading...");
        }

        public void EnterAnId() => _uiProvider.WriteProvider("Enter Id: ");

        public void OkResponse(object value) => _uiProvider.WriteLineProvider($"Data: {value}");

        public void OkListResponse<T>(IEnumerable<T> list)
        {
            _uiProvider.WriteLineProvider("Data:");
            foreach (var item in list)
            {
                _uiProvider.WriteLineProvider(item);
            }
        }

        public void EmptyResponse() => _uiProvider.WriteLineProvider("No data");

        public void ErrorResponse(object value) => _uiProvider.WriteLineProvider($"Error: {value}");
        public string GetInput() => _uiProvider.InputProvider();
    }
}