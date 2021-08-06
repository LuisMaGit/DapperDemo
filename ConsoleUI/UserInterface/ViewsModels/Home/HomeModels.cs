using System;
using System.Threading.Tasks;

namespace ConsoleUI.UserInterface.ViewsModels.Home
{
    public class ActionBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text => $"{Id} - {Name}";
        public Func<Task> Run { get; set; }
    }
    
}