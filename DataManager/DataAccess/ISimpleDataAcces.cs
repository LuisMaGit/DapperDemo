using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManager.DataAccess
{
    public interface ISimpleDataAcces
    {
        Task<List<T>> LoadData<T, TU>(string query, TU parameters);

        Task<bool> SaveData<T>(string query, T parameters);
    }
}