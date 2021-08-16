using System.Collections.Generic;
using DataManager.Domain.Data.Person.Address;


namespace DataManager.Domain.Data.Person
{
    public class FullPerson : Person
    {
        public List<PersonAddress> PersonAddresses { get; set; }

        private string _FormPersonAddressStr()
        {
            var address = "";
            for (var i = 0; i < PersonAddresses.Count; i++)
            {
                if (PersonAddresses[i] != null)
                    address += $"{i}-({PersonAddresses[i].Address}) ";
            }

            return address;
        }

        public override string ToString()
        {
            return $"{base.ToString()}," +
                   $" {nameof(PersonAddresses)}: {_FormPersonAddressStr()}";
        }
    }
}