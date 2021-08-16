using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataManager.Domain;
using DataManager.Domain.Data.Person;
using DataManager.Domain.Data.Person.Address;
using DataManager.Helpers;
using Microsoft.Data.Sqlite;
using DataManager.Queries;

namespace DataManager.DataAccess.PersonServices
{
    public class PersonService : IPersonService
    {
        //Services
        private readonly ISimpleDataAcces _simpleDataAccessService;
        private readonly ConnectionStringHelper _connstr;

        public PersonService(
            ISimpleDataAcces simpleDataAccessService,
            ConnectionStringHelper connstr)
        {
            _simpleDataAccessService = simpleDataAccessService;
            _connstr = connstr;
        }

        public async Task<DataResponseModel<Person>> GetPersonsByIdAsync(int id)
        {
            try
            {
                var persons = await _simpleDataAccessService.LoadData<Person, object>
                    (PersonQueries.GetPersonById(), new {id});
                return DataAccessResponseHelper.SingleDataResponse(persons.Any() ? persons.First() : null);
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.SingleDataException<Person>(c);
            }
        }

        public async Task<DataResponseModel<List<Person>>> SearchPersonsByNameAsync(string name)
        {
            try
            {
                var persons = await _simpleDataAccessService
                    .LoadData<Person, object>(PersonQueries.SearchPersonsByName(),
                        new {name = QueryHelpers.LikeParam(name)});
                return DataAccessResponseHelper.ListDataResponse(persons);
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.ListDataException<Person>(c);
            }
        }

        public async Task<DataResponseModel<Person>> GetPersonByEmailAsync(string email)
        {
            try
            {
                var persons = await _simpleDataAccessService.LoadData<Person, object>(
                    PersonQueries.PersonByEmail(),
                    new {email = QueryHelpers.LikeParam(email)});
                return DataAccessResponseHelper.SingleDataResponse(persons.Any() ? persons.First() : null);
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.SingleDataException<Person>(c);
            }
        }

        public async Task<DataResponseModel<FullPerson>> GetFullPersonByIdAsync(int id)
        {
            FullPerson fullPerson;
            try
            {
                await using var conn = new SqliteConnection(_connstr.GetPersonConnectionString());
                fullPerson = await PersonServiceHelpers.ResolveFullPersonByIdAsync(conn, id);
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.SingleDataException<FullPerson>(c);
            }

            return DataAccessResponseHelper.SingleDataResponse(fullPerson);
        }

        public async Task<DataResponseModel<FullPerson>> SavePersonAsTransactionAsync
            (string name, string lastName, string email, string phone, IEnumerable<string> addresses)
        {
            try
            {
                await using var conn = new SqliteConnection(_connstr.GetPersonConnectionString());
                //Begin Transaction
                conn.Open();
                await using var transaction = conn.BeginTransaction();
                //Insert person
                var id = await PersonServiceHelpers.InsertPersonAsync(conn, name, lastName, email, phone);
                if (id == -1)
                {
                    return DataAccessResponseHelper.SingleDataException<FullPerson>(
                        new Exception("Error Inserting person: Id == -1"));
                }

                //Insert Addresses
                var succesAddress = await PersonServiceHelpers.InsertAddressesWithTransactionAsync(conn, id, addresses);
                //Error inserting address
                if (!succesAddress)
                {
                    //Error Inserting Address
                    transaction.Rollback();
                    return DataAccessResponseHelper.SingleDataException<FullPerson>(
                        new Exception("Error Inserting address"));
                }

                //Get Full Person by id, and handle transaction
                var fullPerson =
                    await PersonServiceHelpers.HandleGetFullPersonByIdWithTransactionAsync(conn, transaction, id);
                if (fullPerson == null)
                {
                    return DataAccessResponseHelper.SingleDataException<FullPerson>(
                        new Exception("Error Getting full person"));
                }

                return DataAccessResponseHelper.SingleDataResponse(fullPerson);
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.SingleDataException<FullPerson>(c);
            }
        }

        public async Task<DataResponseModel<List<Person>>> GetPersonsPaginatedAsync(int page)
        {
            try
            {
                var personPaginated =
                    await _simpleDataAccessService.LoadData<PersonPaginated, object>
                    (PersonQueries.PersonPaginated(),
                        PaginationHelper.GetPaginationParams(page));

                return DataAccessResponseHelper
                    .HandlePaginationResponse<Person, PersonPaginated>(personPaginated, page);
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.ListDataException<Person>(c);
            }
        }

        public async Task<DataResponseModel<List<FullPerson>>> GetFullPersonPaginatedAsync(int page)
        {
            return await PersonServiceHelpers.ResolveFullPersonPaginationResponse
            (_connstr.GetPersonConnectionString(),
                PersonQueries.FullPersonPaginated(),
                PaginationHelper.GetPaginationParams(page),
                page);
        }

        public async Task<DataResponseModel<List<FullPerson>>> SearchAddressFullPersonPaginatedAsync
            (int page, string address)
        {
            var param = new
            {
                search = QueryHelpers.LikeParam(address),
                limit = PaginationHelper.SIZE_PAGE,
                offset = PaginationHelper.GetOffset(page),
            };
            return await PersonServiceHelpers.ResolveFullPersonPaginationResponse
            (_connstr.GetPersonConnectionString(),
                PersonQueries.SearchAddressFullPersonPaginated(),
                param,
                page);
        }
    }

    internal static class PersonServiceHelpers
    {
        internal static async Task<DataResponseModel<List<FullPerson>>> ResolveFullPersonPaginationResponse(
            string connectionStr, string query, object param, int page)
        {
            try
            {
                var fullPersons = new List<FullPersonPaginated>();
                await using var connection = new SqliteConnection(connectionStr);
                await connection.QueryAsync<FullPersonPaginated, PersonAddress, List<FullPersonPaginated>>
                (query, (fullPerson, address) =>
                    {
                        //Check if it was previously added
                        var indexWithAddress = fullPersons.FindIndex(p => p.Id == fullPerson.Id);
                        if (indexWithAddress != -1)
                        {
                            fullPersons[indexWithAddress].PersonAddresses ??= new List<PersonAddress>();
                            fullPersons[indexWithAddress].PersonAddresses.Add(address);
                            return fullPersons;
                        }

                        fullPerson.PersonAddresses ??= new List<PersonAddress>();
                        fullPerson.PersonAddresses.Add(address);
                        fullPersons.Add(fullPerson);
                        return fullPersons;
                    },
                    param);

                return DataAccessResponseHelper
                    .HandlePaginationResponse<FullPerson, FullPersonPaginated>(fullPersons, page);
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.ListDataException<FullPerson>(c);
            }
        }

        internal static async Task<FullPerson> ResolveFullPersonByIdAsync(IDbConnection conn, int id)
        {
            FullPerson fullPerson = default;
            await conn.QueryAsync<FullPerson, PersonAddress, FullPerson>(
                PersonQueries.FullPersonById(),
                (person, address) =>
                {
                    fullPerson ??= person;
                    fullPerson.PersonAddresses ??= new List<PersonAddress>();
                    fullPerson.PersonAddresses.Add(address);

                    return fullPerson;
                },
                new {personId = id});

            return fullPerson;
        }

        internal static async Task<bool> InsertAddressesWithTransactionAsync(
            IDbConnection conn, int id, IEnumerable<string> addresses)
        {
            foreach (var address in addresses)
            {
                try
                {
                    await conn.ExecuteAsync(PersonQueries.InsertAddress(), new {personId = id, address});
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        internal static async Task<int> InsertPersonAsync(
            IDbConnection conn, string name, string lastName, string email, string phone)
        {
            try
            {
                var insertParams = new DynamicParameters();
                insertParams.Add($"@{nameof(name)}", name);
                insertParams.Add($"@{nameof(lastName)}", lastName);
                insertParams.Add($"@{nameof(email)}", email);
                insertParams.Add($"@{nameof(phone)}", phone);

                var queryPlusLastId = PersonQueries.InsertPerson() + QueryHelpers.LastInsertedId();

                var respnonse = await conn.QueryAsync<int>(queryPlusLastId, insertParams);
                return respnonse.First();
            }
            catch
            {
                return -1;
            }
        }

        internal static async Task<FullPerson> HandleGetFullPersonByIdWithTransactionAsync(
            IDbConnection conn, IDbTransaction transaction, int id)
        {
            FullPerson fullPerson;
            try
            {
                fullPerson = await ResolveFullPersonByIdAsync(conn, id);
            }
            catch
            {
                transaction.Rollback();
                return null;
            }

            transaction.Commit();
            return fullPerson;
        }
    }
}