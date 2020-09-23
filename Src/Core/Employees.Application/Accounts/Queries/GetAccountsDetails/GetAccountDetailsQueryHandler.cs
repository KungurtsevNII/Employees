﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Employees.Application.Configuration.Queries;
using Employees.Domain.Accounts;

namespace Employees.Application.Accounts.Queries.GetAccountsDetails
{
    public class GetAccountDetailsQueryHandler
        : IQueryHandler<GetAccountDetailsQuery, AccountDetailsDto>
    {
        private readonly IAccountsRepository _accountsRepository;

        public GetAccountDetailsQueryHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<AccountDetailsDto> Handle(GetAccountDetailsQuery query, CancellationToken cancellationToken)
        {
            var account = await _accountsRepository.GetByIdAsync(query.AccountId);
            return new AccountDetailsDto
            {
                Email = account.Email,
                Id = account.Id
            };
        }
    }
}