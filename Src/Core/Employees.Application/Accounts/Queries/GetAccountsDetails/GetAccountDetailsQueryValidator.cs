using FluentValidation;

namespace Employees.Application.Accounts.Queries.GetAccountsDetails
{
    public class GetAccountDetailsQueryValidator : AbstractValidator<GetAccountDetailsQuery>
    {
        public GetAccountDetailsQueryValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }
}