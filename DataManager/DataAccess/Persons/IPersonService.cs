using System.Collections.Generic;
using System.Threading.Tasks;
using DataManager.Domain;
using DataManager.Domain.Data;

namespace DataManager.DataAccess.Persons
{
    public interface IPersonService
    {
        Task<DataResponseModel<Person>> GetPersonsByIdAsync(int id);
        Task<DataResponseModel<List<Person>>> SearchPersonsByName(string id);
        
    }
}