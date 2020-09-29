using System;
using System.Threading;
using System.Threading.Tasks;
using Employees.Application.Configuration;
using Employees.Application.Configuration.Commands;
using Employees.Application.Configuration.Exceptions;
using Employees.Domain.Accounts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Employees.Application.Accounts.Commands.RegisterAccount
{
    public class RegisterAccountCommandHandler : ICommandHandler<RegisterAccountCommand>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly ILogger<RegisterAccountCommandHandler> _logger;

        public RegisterAccountCommandHandler(IAccountsRepository accountsRepository, ILogger<RegisterAccountCommandHandler> logger)
        {
            _accountsRepository = accountsRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
        {
            if (await _accountsRepository.IsExistsByEmailAsync(command.Email))
            {
                throw new BadRequestException($"Email - {command.Email} уже есть в системе.");
            }

            if (await _accountsRepository.IsExistsByPhoneAsync(command.PhoneNumber))
            {
                throw new BadRequestException($"Телефон - {command.PhoneNumber} уже есть в системе.");
            }

            await _accountsRepository.CreateAsync(new Account
            {
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                IsEmailConfirm = false,
                IsPhoneConfirm = false
            });
            
            _logger.LogInformation($"Успешно создан пользователь с Email - {command.Email}");
            
            return Unit.Value;
        }
    }
}