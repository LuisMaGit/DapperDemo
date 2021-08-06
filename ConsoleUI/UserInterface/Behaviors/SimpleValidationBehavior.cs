using System;
using ConsoleUI.UserInterface.Services;

namespace ConsoleUI.UserInterface.Behaviors
{
    public class SimpleValidationBehavior
    {
        //Service
        private readonly ApplicationUiService _appUi;

        //Props
        private bool _isValid;

        public SimpleValidationBehavior(ApplicationUiService appUi)
        {
            _appUi = appUi;
        }

        public void ValidationLogic(Action enterValueView, Func<string, bool> validateInput)
        {
            do
            {
                enterValueView();
                var input = _appUi.GetInput();
                _isValid = validateInput(input);
                if (!_isValid)
                {
                    _appUi.InvalidValue();
                }
            } while (!_isValid);
        }
    }
}