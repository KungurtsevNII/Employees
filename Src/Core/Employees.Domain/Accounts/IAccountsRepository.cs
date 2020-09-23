using System;
using System.Threading.Tasks;

namespace Employees.Domain.Accounts
{
    public interface IAccountsRepository
    {
        Task<Account> GetByIdAsync(Guid accountId);
    }
}