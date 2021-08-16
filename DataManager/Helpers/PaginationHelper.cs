namespace DataManager.Helpers
{
    public static class PaginationHelper
    {
        public const int SIZE_PAGE = 10;

        public static int GetOffset(int page)
        {
            return SIZE_PAGE * (page - 1);
        }

        public static object GetPaginationParams(int page)
        {
            return new {limit = SIZE_PAGE, offset = GetOffset(page)};
        }
    }
}