using System.Collections.Generic;
using System.Threading.Tasks;
using DataManager.Domain;
using DataManager.Domain.Data.Person;

namespace DataManager.DataAccess.PersonServices
{
    public interface IPersonService
    {
        Task<DataResponseModel<Person>> GetPersonsByIdAsync(int id);
        
        Task<DataResponseModel<List<Person>>> SearchPersonsByNameAsync(string id);
        
        Task<DataResponseModel<Person>> GetPersonByEmailAsync(string email);
        
        Task<DataResponseModel<FullPerson>> GetFullPersonByIdAsync(int id);
        
        Task<DataResponseModel<FullPerson>> SavePersonAsTransactionAsync
            (string name, string lastName, string email, string phone, IEnumerable<string> addresses);
        
        Task<DataResponseModel<List<Person>>> GetPersonsPaginatedAsync(int page);
        
        Task<DataResponseModel<List<FullPerson>>> GetFullPersonPaginatedAsync(int page);
        Task<DataResponseModel<List<FullPerson>>> SearchAddressFullPersonPaginatedAsync(int page, string address);
    }
}