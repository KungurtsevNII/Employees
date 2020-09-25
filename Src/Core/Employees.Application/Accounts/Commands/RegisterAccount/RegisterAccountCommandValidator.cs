using Employees.Application.Configuration.Extensions;
using Employees.Application.Configuration.Validation;
using FluentValidation;

namespace Employees.Application.Accounts.Commands.RegisterAccount
{
    public class RegisterAccountCommandValidator : BaseValidator<RegisterAccountCommand>
    {
        public RegisterAccountCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Must(IsPhoneValid).WithPhoneNumberValidationErrorMessage()
                .Length(_phonenumberLength);
        }
    }
}