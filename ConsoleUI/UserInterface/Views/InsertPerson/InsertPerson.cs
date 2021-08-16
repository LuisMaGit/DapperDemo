using ConsoleUI.UserInterface.Helpers;
using ConsoleUI.UserInterface.Services.UIProvider;

namespace ConsoleUI.UserInterface.Views.InsertPerson
{
    public class InsertPerson
    {
        private readonly IUIProvider _uiProvider;
        
        public InsertPerson(IUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
        }

        public void EnterName()
        {
            _uiProvider.WriteProvider("Enter name (required): ");
        }        
        
        public void EnterLastName()
        {
            _uiProvider.WriteProvider("Enter last name: ");
        }        
        
        public void EnterEmail()
        {
            _uiProvider.WriteProvider("Enter email (required): ");
        }        
        
        public void EnterPhone()
        {
            _uiProvider.WriteProvider("Enter phone number: ");
        }       
        
        public void EnterAdresses()
        {
            _uiProvider.WriteLineProvider($"Enter addresses ({Constants.EXIT_CHAR} to finish)");
        }        
        public void EnterAdress()
        {
            _uiProvider.WriteProvider("Addresses:");
        }
    }
}