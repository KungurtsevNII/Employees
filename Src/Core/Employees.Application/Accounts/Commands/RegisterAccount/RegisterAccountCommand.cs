using Employees.Application.Configuration.Commands;

namespace Employees.Application.Accounts.Commands.RegisterAccount
{
    public class RegisterAccountCommand : ICommand
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}