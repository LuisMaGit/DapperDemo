using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Behaviors.PaginationViewBehavior
{
    public class PaginationView
    {
        private readonly IUIProvider _uiProvider;

        public PaginationView(IUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
        }

        public void Total(int total)
        {
            _uiProvider.WriteLineProvider($"Total: {total}");
        }

        public void Pagination(int currentPage, int totalPages)
        {
            _uiProvider.WriteLineProvider($"Current Page: {currentPage}");
            _uiProvider.WriteLineProvider($"Total of Pages: {totalPages}");
        }

        public void InsertPageNumber(int firstPage, int lastPage)
        {
            _uiProvider.WriteLineProvider($"Insert page beetween {firstPage} and {lastPage} " +
                                          $"({Constants.EXIT_CHAR} to break): ");
        }
    }
}