using System;

namespace DataManager.Queries
{
    public abstract class PersonQueries
    {
        public static string GetPersonById()
        {
            return @"SELECT * 
                    FROM Person
                    WHERE Id == @id";
        }

        public static string SearchPersonsByName()
        {
            return @"SELECT * 
                    FROM Person 
                    WHERE Name LIKE @name or LastName LIKE @name";
        }
    }
}