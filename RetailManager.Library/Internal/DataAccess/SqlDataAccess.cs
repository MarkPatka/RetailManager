using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Dapper;

namespace RetailManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess
    {
        public string GetConnectionString(string name) =>
            ConfigurationManager.ConnectionStrings[name].ConnectionString;

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionString)
        {
            string connectionStringName = GetConnectionString(connectionString);

            using (IDbConnection connection = new SqlConnection(connectionStringName))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure)
                    .ToList();

                return rows;
            }
        }
        public void SaveData<T>(string storedProcedure, T parameters, string connectionString)
        {
            string connectionStringName = GetConnectionString(connectionString);

            using (IDbConnection connection = new SqlConnection(connectionStringName))
            {
                connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
