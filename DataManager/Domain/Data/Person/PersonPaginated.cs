namespace DataManager.Domain.Data.Person
{
    public class PersonPaginated : Person, IPaginationBaseFields
    {
        public int TotalCount { get; set; }
    }
}