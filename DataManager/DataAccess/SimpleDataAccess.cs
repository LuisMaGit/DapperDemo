using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataManager.Helpers;
using Microsoft.Data.Sqlite;

namespace DataManager.DataAccess
{
    public class SimpleDataAccess : ISimpleDataAcces
    {
        private readonly string _connstr;

        public SimpleDataAccess(ConnectionStringHelper connstr)
        {
            _connstr = connstr.GetPersonConnectionString();
        }

        public async Task<List<T>> LoadData<T, TU>(string query, TU parameters)
        {
            await using var connection = new SqliteConnection(_connstr);
            var data = await connection.QueryAsync<T>(query, parameters);
            return data.ToList();
        }

        public async Task SaveData<T>(string query, T parameters)
        {
            await using var connection = new SqliteConnection(_connstr);
            await connection.ExecuteAsync(query, parameters);
        }
    }
}