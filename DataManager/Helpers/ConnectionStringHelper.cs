using System;
using Microsoft.Extensions.Configuration;

namespace DataManager.Helpers
{
    public class ConnectionStringHelper
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string  GetPersonConnectionString()
        {
            return _configuration.GetConnectionString("PersonsDb");
        }
    }
}