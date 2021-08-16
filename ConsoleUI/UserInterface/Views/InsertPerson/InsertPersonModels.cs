using System;

namespace ConsoleUI.UserInterface.Views.InsertPerson
{
    public enum InsertPersonActions
    {
        Name,
        LastName,
        Email,
        Phone,
        Addresses,
    };

    public class InsertPersonActionsModels
    {
        public Action InsertSign { get; set; }
        public Func<string, bool> Validation { get; set; }
        public string Value { get; set; }
    }
}