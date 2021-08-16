using System;
using ConsoleUI.UserInterface.Services;

namespace ConsoleUI.UserInterface.Behaviors
{
    public class SimpleValidationBehavior
    {
        //Service
        private readonly ApplicationUiService _appUi;

        //Props

        public SimpleValidationBehavior(ApplicationUiService appUi)
        {
            _appUi = appUi;
        }

        public string ValidationLogic(Action enterValueView, Func<string, bool> validateInput)
        {
            bool isValid;
            string input;
            do
            {
                enterValueView();
                input = _appUi.GetInput();
                isValid = validateInput(input);
                if (!isValid)
                {
                    _appUi.InvalidValue();
                }
            } while (!isValid);

            return input;
        }
    }
}