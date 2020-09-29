using FluentValidation;

namespace Employees.Application.Accounts.Commands.ConfirmationEmail
{
    public class ConfirmationEmailCommandValidator : AbstractValidator<ConfirmationEmailCommand>
    {
        public ConfirmationEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}