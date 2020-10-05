using System;
using System.Threading.Tasks;

namespace Employees.Domain.Accounts
{
    public interface IAccountsRepository
    {
        Task<Account> GetByIdAsync(Guid accountId);
        Task<Account> GetByEmailAsync(string email);
        Task<bool> IsExistsAsync(Guid accountId);
        Task<bool> IsExistsByEmailAsync(string email);
        Task<bool> IsExistsByPhoneAsync(string phone);
        Task CreateAsync(Account account);
        Task SetIsEmailConfirm(bool isEmailConfirm, Guid accountId);
    }
}