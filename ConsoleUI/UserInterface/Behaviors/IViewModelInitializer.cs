using System.Threading.Tasks;

namespace ConsoleUI.UserInterface.Behaviors
{
    public interface IViewModelInitializer
    {
        Task InitViewModel();
    }
}