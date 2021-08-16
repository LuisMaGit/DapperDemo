namespace DataManager.Domain.Data.Person
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}," +
                   $" {nameof(Name)}: {Name}," +
                   $" {nameof(LastName)}: {LastName}," +
                   $" {nameof(Email)}: {Email}," +
                   $" {nameof(Phone)}: {Phone}";
        }
    }
}