namespace ConsoleUI.UserInterface.Services.UIProvider
{
    public interface IUIProvider
    {
        void WriteProvider(object action);
        void WriteLineProvider(object action);
        string InputProvider();
    }
}