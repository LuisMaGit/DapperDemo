using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI.UserInterface.Helpers
{
    public abstract class Validators
    {
        public static bool ValidateMenuSelection(string id, IEnumerable<int> range)
        {
            var isValid = ValidateInt(id, out var idInt);
            return isValid && range.Contains(idInt);
        }

        public static bool ValidateInt(string id, out int idInt)
        {
            try
            {
                idInt = int.Parse(id);
                return true;
            }
            catch
            {
                idInt = 0;
                return false;
            }
        }

        public static bool NameValidator(string input)
        {
            return input.Length <= 100;
        }
    }
}