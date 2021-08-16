namespace DataManager.Domain.Data.Person
{
    public class FullPersonPaginated : FullPerson, IPaginationBaseFields
    {
        public int TotalCount { get; set; }
    }
}