using System;
using Employees.Application.Configuration.Queries;

namespace Employees.Application.Accounts.Queries.GetAccountsDetails
{
    public class GetAccountDetailsQuery : IQuery<AccountDetailsDto>
    {
        public Guid AccountId { get; set; }
    }
}