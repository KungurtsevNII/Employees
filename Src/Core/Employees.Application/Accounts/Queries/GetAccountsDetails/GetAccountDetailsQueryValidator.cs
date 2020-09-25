using Employees.Application.Configuration.Validation;
using FluentValidation;

namespace Employees.Application.Accounts.Queries.GetAccountsDetails
{
    public class GetAccountDetailsQueryValidator : BaseValidator<GetAccountDetailsQuery>
    {
        public GetAccountDetailsQueryValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }
}