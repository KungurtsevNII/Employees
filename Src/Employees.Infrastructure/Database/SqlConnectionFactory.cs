using System.Data;
using System.Data.SqlClient;

namespace Employees.Infrastructure.Database
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        { 
            return new SqlConnection(_connectionString);
        }
    }
}