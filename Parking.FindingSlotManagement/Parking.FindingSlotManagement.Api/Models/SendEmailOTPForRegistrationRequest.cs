using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Api.Models
{
    public class SendEmailOTPForRegistrationRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
