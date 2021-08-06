using System.Threading.Tasks;
using ConsoleUI.UserInterface.Services;

namespace ConsoleUI.UserInterface.Behaviors.SimpleViewBehavior
{
    //T - View asociated with these VM
    public abstract class SimpleViewModelBehavior<T> where T : SimpleViewBehavior
    {
        //Services
        private readonly ApplicationUiService _appUi;
        private readonly SimpleValidationBehavior _validationBehavior;
        private readonly T _view;

        protected SimpleViewModelBehavior(
            ApplicationUiService appUi, 
            SimpleValidationBehavior validationBehavior, 
            T view)
        {
            _appUi = appUi;
            _validationBehavior = validationBehavior;
            _view = view;
        }
        

        protected abstract bool ValidateInput(string input);
        protected abstract Task RunServiceLogic();

        public async Task InitViewModel()
        {
            _validationBehavior.ValidationLogic(_view.EnterValue, ValidateInput);
            _appUi.Loading();
            await RunServiceLogic();
        }
    }
}