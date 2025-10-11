using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.AuthenticationManagement.Queries.CheckPhone
{
    public class CheckPhoneValidation : AbstractValidator<CheckPhoneQuery>
    {
        public CheckPhoneValidation()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .NotNull().WithMessage("Phone number is required")
                .Must(BeValidPhoneNumber).WithMessage("Phone number must be 10 digits starting with 0 or +84 followed by 9 digits");
        }

        private bool BeValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return false;
            
            // Vietnamese local format: 0xxxxxxxxx (10 digits)
            if (phoneNumber.StartsWith("0") && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit))
                return true;
            
            // International format: +84xxxxxxxxx (12 characters)
            if (phoneNumber.StartsWith("+84") && phoneNumber.Length == 12 && phoneNumber.Substring(3).All(char.IsDigit))
                return true;
                
            return false;
        }
    }
}
