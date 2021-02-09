using System;
using System.Linq;
using FluentValidation;

namespace Employees.Application.Configuration.Validation
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected readonly int _phonenumberLength = 12;
        
        protected bool IsPhoneValid(string phone)
        {
            return !(!phone.StartsWith("+79")
                     || !phone.Substring(1).All(c => Char.IsDigit(c)));
        }
    }
}