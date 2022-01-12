using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;

namespace TVShowTracker.Webapi.Application.Services
{
    internal static class ConnectorService
    {
        internal static string GetMySqlConnectionString(IConfiguration configuration) 
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string connection = configuration.GetConnectionString("DefaultConnection");

            if (environmentName == "Production")
                connection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

            return connection;
        }

        internal static MySqlConnection GetMySqlConnection(IConfiguration configuration) 
        {
            MySqlConnection connection = new MySqlConnection(GetMySqlConnectionString(configuration));
            connection.Open();

            return connection;
        }
    }
}
