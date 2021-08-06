using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataManager.Domain;
using DataManager.Domain.Data;
using DataManager.Helpers;

namespace DataManager.DataAccess.Persons
{
    public class PersonService : IPersonService
    {
        //Services
        private readonly ISimpleDataAcces _simpleDataAccessService;

        //Props
        private List<Person> _persons;

        public PersonService(ISimpleDataAcces simpleDataAccessService)
        {
            _simpleDataAccessService = simpleDataAccessService;
        }
        
        public async Task<DataResponseModel<Person>> GetPersonsByIdAsync(int id)
        {
            try
            {
                _persons = await _simpleDataAccessService.LoadData<Person, object>
                    (Queries.PersonQueries.GetPersonById(), new {id});
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.SingleDataException<Person>(c);
            }

            return DataAccessResponseHelper.SingleDataResponse(_persons);
        }

        public async Task<DataResponseModel<List<Person>>> SearchPersonsByName(string name)
        {
            try
            {
                _persons = await _simpleDataAccessService
                    .LoadData<Person, object>(Queries.PersonQueries.SearchPersonsByName(), new {name = "%" + name + "%" });
            }
            catch (Exception c)
            {
                return DataAccessResponseHelper.ListDataException<Person>(c);
            }

            return DataAccessResponseHelper.ListDataResponse(_persons);
        }
    }
}