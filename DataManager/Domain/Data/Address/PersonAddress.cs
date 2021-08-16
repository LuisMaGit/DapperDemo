namespace DataManager.Domain.Data.Person.Address
{
    public class PersonAddress
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, " +
                   $"{nameof(PersonId)}: {PersonId}," +
                   $" {nameof(Address)}: {Address}";
        }
    }
}