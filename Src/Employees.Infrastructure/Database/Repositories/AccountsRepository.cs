using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Employees.Domain.Accounts;
using Employees.Infrastructure.Database;
using Microsoft.Extensions.Configuration;

namespace Employees.Infrastructure.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;

        public AccountsRepository(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Account> GetByIdAsync(Guid accountId)
        {
            using (var connection = _sqlConnectionFactory.GetConnection())
            {
                connection.Open();
                return await connection.QueryFirstAsync<Account>("SELECT * FROM Accounts WHERE Id = @accountId",
                    new {accountId});
            }
        }
    }
}