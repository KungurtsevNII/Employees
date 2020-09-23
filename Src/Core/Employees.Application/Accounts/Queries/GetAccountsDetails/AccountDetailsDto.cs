using System;

namespace Employees.Application.Accounts.Queries.GetAccountsDetails
{
    public class AccountDetailsDto
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
    }
}