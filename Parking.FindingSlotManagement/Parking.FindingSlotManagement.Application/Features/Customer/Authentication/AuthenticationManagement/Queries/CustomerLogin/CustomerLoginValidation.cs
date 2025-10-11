using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.AuthenticationManagement.Queries.CustomerLogin
{
    public class CustomerLoginValidation : AbstractValidator<CustomerLoginQuery>
    {
        public CustomerLoginValidation()
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .NotNull().WithMessage("Phone is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password is required");
        }
    }
}
