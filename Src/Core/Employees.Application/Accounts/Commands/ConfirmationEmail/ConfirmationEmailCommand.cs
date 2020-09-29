using Employees.Application.Configuration.Commands;

namespace Employees.Application.Accounts.Commands.ConfirmationEmail
{
    public class ConfirmationEmailCommand : ICommand
    {
        public string Email { get; set; }
    }
}