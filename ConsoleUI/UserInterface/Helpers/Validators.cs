using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

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

        public static bool ValidateName(string input)
        {
            return input.Length > 0 && input.Length <= 100;
        }

        public static bool ValidateLastName(string input)
        {
            return input.Length <= 100;
        }

        public static bool ValidateAddress(string input)
        {
            return input.Length > 0 && input.Length <= 200;
        }

        public static bool ValidateEmail(string input, out string email)
        {
            try
            {
                var validMail = new MailAddress(input);
                email = validMail.Address;
            }
            catch
            {
                email = "";
                return false;
            }

            return true;
        }

        public static bool ValidateEmail(string input)
        {
            return ValidateEmail(input, out _);
        }

        public static bool ValidatePhone(string input)
        {
            var match = new Regex(Constants.REGULAR_EXP_PHONE).Match(input).Success;
            return match;
        }

        public static bool ValidatePageRange(string input, int lastPage)
        {
            if (input == Constants.EXIT_CHAR) return true;

            var isInt = ValidateInt(input, out var page);
            if (!isInt) return false;

            return page >= 1 && page <= lastPage;
        }
    }
}