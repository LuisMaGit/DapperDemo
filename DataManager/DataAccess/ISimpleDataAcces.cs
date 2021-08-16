using System.Collections.Generic;
using System.Threading.Tasks;
using DataManager.Domain;

namespace DataManager.DataAccess
{
    public interface ISimpleDataAcces
    {
        Task<List<T>> LoadData<T, TU>(string query, TU parameters);
        Task SaveData<T>(string query, T parameters);
    }
}