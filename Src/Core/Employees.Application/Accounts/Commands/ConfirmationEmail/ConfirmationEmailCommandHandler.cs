using System.Threading;
using System.Threading.Tasks;
using Employees.Application.Configuration.Commands;
using Employees.Application.Configuration.Exceptions;
using Employees.Domain.Accounts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Employees.Application.Accounts.Commands.ConfirmationEmail
{
    public class ConfirmationEmailCommandHandler : ICommandHandler<ConfirmationEmailCommand>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly ILogger<ConfirmationEmailCommandHandler> _logger;

        public ConfirmationEmailCommandHandler(IAccountsRepository accountsRepository, 
            ILogger<ConfirmationEmailCommandHandler> logger)
        {
            _accountsRepository = accountsRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(ConfirmationEmailCommand command, CancellationToken ct)
        {
            if (!await _accountsRepository.IsExistsByEmailAsync(command.Email))
            {
                throw new NotFoundException(Account.EntityName, command.Email);
            }

            var account = await _accountsRepository.GetByEmailAsync(command.Email);
            account.ConfirmEmail();
            await _accountsRepository.SetIsEmailConfirm(account.IsEmailConfirm, account.Id);
            
            _logger.LogInformation($"Email - {command.Email} подтвержден.");
            
            return Unit.Value;
        }
    }
}