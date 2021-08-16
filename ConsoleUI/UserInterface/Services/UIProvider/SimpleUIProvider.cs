using System;

namespace ConsoleUI.UserInterface.Services.UIProvider
{
    public class SimpleUIProvider : IUIProvider
    {
        public void WriteProvider(object action)
        {
            Console.Write(action);
        }
        
        public void WriteLineProvider(object action)
        {
            Console.WriteLine(action);
        }

        public string InputProvider()
        {
            return Console.ReadLine();
        }
    }

}