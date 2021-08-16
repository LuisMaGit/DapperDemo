namespace DataManager.Queries
{
    public abstract class PersonQueries
    {
        public static string GetPersonById()
        {
            return @"SELECT * 
                    FROM Persons
                    WHERE Id == @id";
        }

        public static string SearchPersonsByName()
        {
            return @"SELECT * 
                    FROM Persons
                    WHERE Name LIKE @name or LastName LIKE @name";
        }

        public static string PersonByEmail()
        {
            return @"Select * From Persons WHERE Email Like @email";
        }

        public static string FullPersonById()
        {
            return @"SELECT * 
                      FROM Persons p
                      LEFT JOIN Addresses pa on p.Id = pa.PersonId 
                      WHERE p.Id == @personId";
        }


        public static string InsertPerson()
        {
            return @"INSERT INTO Persons(Name,LastName,Email,Phone) 
                     VALUES (@name,@lastName,@email,@phone);";
        }

        public static string InsertAddress()
        {
            return @"INSERT INTO Addresses (PersonId, Address)
                     VALUES (@personId, @address)";
        }

        public static string PersonPaginated()
        {
            return @"SELECT *, COUNT() OVER () AS TotalCount
                     FROM Persons p
                        ORDER BY p.Id
                        LIMIT @limit OFFSET @offset;
                    ";
        }

        public static string FullPersonPaginated()
        {
            return @"SELECT *
                        FROM (SELECT *, COUNT() OVER () AS TotalCount
                              FROM Persons p
                              ORDER BY p.Id
                              LIMIT @limit OFFSET @offset) ps
                        LEFT JOIN Addresses pa on ps.Id = pa.PersonId;";
        }

        public static string SearchAddressFullPersonPaginated()
        {
            return @"SELECT p.*, TotalCount, pas.*
                        FROM (SELECT *, COUNT() OVER () AS TotalCount
                              FROM Addresses pa
                              WHERE pa.Address LIKE @search
                              ORDER BY pa.id
                              LIMIT @limit OFFSET @offset) pas, Persons p
                        WHERE p.Id = pas.PersonId;";
        }
    }
}