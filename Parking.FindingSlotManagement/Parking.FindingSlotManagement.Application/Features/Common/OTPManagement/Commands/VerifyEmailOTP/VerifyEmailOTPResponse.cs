using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyEmailOTP
{
    public class VerifyEmailOTPResponse
    {
        public bool Verified { get; set; }
        public string? Token { get; set; }
    }
}
