namespace DataManager.Domain
{
    public class PaginationResponseModel
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}