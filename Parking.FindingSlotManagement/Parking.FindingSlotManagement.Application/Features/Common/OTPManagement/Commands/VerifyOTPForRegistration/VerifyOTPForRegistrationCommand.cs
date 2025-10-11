using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyOTPForRegistration
{
    public class VerifyOTPForRegistrationCommand : IRequest<ServiceResponse<VerifyOTPForRegistrationResponse>>
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
    }
}
