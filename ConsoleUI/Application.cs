using System.Threading.Tasks;
using ConsoleUI.UserInterface.Views.Home;

namespace ConsoleUI
{
    public class Application
    {
        private readonly HomeViewModel _applicationView;

        public Application(HomeViewModel applicationView)
        {
            _applicationView = applicationView;
        }

        public async Task Run()
        {
            while (true)
            {
                await _applicationView.AskForActionAsync();
            }
        }
    }
}