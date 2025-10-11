using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.Commands.CustomerRegisterWithToken
{
    public class CustomerRegisterWithTokenResponse
    {
        public bool Success { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
