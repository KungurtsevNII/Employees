using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Employees.Domain.Accounts;
using Employees.Infrastructure.Database;

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
            using var connection = _sqlConnectionFactory.GetConnection();
            connection.Open();
            return await connection.QueryFirstAsync<Account>(
                @"SELECT Id, Email, Phonenumber, IsPhonenumberConfirm, IsEmailConfirm
                       FROM Accounts WITH(NOLOCK) WHERE Id = @accountId",
                new {accountId});
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            using var connection = _sqlConnectionFactory.GetConnection();
            connection.Open();
            return await connection.QueryFirstAsync<Account>(
                @"SELECT Id, Email, Phonenumber, IsPhonenumberConfirm, IsEmailConfirm
                       FROM Accounts WITH(NOLOCK) WHERE Email = @email",
                new {email});
        }

        public async Task<bool> IsExistsAsync(Guid accountId)
        {
            using var connection = _sqlConnectionFactory.GetConnection();
            connection.Open();
            var count = await connection.QueryFirstAsync<int>(
                "SELECT COUNT(Id) FROM Accounts WITH(NOLOCK) WHERE Id = @accountId",
                new {accountId});

            return count > 0;
        }

        public async Task<bool> IsExistsByEmailAsync(string email)
        {
            using var connection = _sqlConnectionFactory.GetConnection();
            connection.Open();
            var count = await connection.QueryFirstAsync<int>(
                "SELECT COUNT(Id) FROM Accounts WITH(NOLOCK) WHERE Email = @email",
                new {email});

            return count > 0;
        }

        public async Task<bool> IsExistsByPhoneAsync(string phone)
        {
            using var connection = _sqlConnectionFactory.GetConnection();
            connection.Open();
            var count = await connection.QueryFirstAsync<int>(
                "SELECT COUNT(Id) FROM Accounts WITH(NOLOCK) WHERE Phonenumber = @phone",
                new {phone});

            return count > 0;
        }

        public async Task CreateAsync(Account account)
        {
            using var connection = _sqlConnectionFactory.GetConnection();
            connection.Open();
            var sql =
                @"INSERT INTO [dbo].[Accounts]
                    (Email,
                     Phonenumber,
                     IsPhonenumberConfirm,
                     IsEmailConfirm)
                  VALUES
                    (@Email,
                     @PhoneNumber,
                     @IsPhoneConfirm,
                     @IsEmailConfirm)";

            await connection.ExecuteAsync(sql, account);
        }

        public async Task SetIsEmailConfirm(bool isEmailConfirm, Guid accountId)
        {
            using var connection = _sqlConnectionFactory.GetConnection();
            connection.Open();
            var sql =
                @"UPDATE [dbo].[Accounts]
                  SET IsEmailConfirm = @isEmailConfirm
                  WHERE Id = @accountId";

            var sqlParams = new {IsEmailConfirm = isEmailConfirm, AccountId = accountId};
            await connection.ExecuteAsync(sql, sqlParams);
        }
    }
}