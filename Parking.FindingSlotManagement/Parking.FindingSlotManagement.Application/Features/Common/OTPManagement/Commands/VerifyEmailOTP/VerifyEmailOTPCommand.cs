using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyEmailOTP
{
    public class VerifyEmailOTPCommand : IRequest<ServiceResponse<VerifyEmailOTPResponse>>
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
