using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.Commands.CustomerRegisterWithToken
{
    public class CustomerRegisterWithTokenCommand : IRequest<ServiceResponse<CustomerRegisterWithTokenResponse>>
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
