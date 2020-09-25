using FluentValidation;
using FluentValidation.Resources;

namespace Employees.Application.Configuration.Extensions
{
    public static class RuleBuilderOptionsExtension
    {
        public static IRuleBuilderOptions<T, TProperty> WithPhoneNumberValidationErrorMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.Configure(config => {
                config.CurrentValidator.Options.ErrorMessageSource = new StaticStringSource("Ожидается следующий формат телефона - +7XXXXXXXXXX.");
            });
        }
    }
}