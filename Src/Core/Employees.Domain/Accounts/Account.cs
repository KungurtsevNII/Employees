using System;

namespace Employees.Domain.Accounts
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPhoneConfirm { get; set; }
        public bool IsEmailConfirm { get; set; }
    }
}