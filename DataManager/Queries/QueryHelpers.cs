namespace DataManager.Queries
{
    public abstract class QueryHelpers
    {
        public static string LikeParam(string value)
        {
            return "%" + value + "%";
        }

        public static string LastInsertedId()
        {
            return "SELECT last_insert_rowid() as id";
        }
    }
}