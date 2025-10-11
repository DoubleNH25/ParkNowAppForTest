using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyEmailOTP
{
    public class VerifyEmailOTPCommandHandler : IRequestHandler<VerifyEmailOTPCommand, ServiceResponse<VerifyEmailOTPResponse>>
    {
        private readonly IOTPRepository _otpRepository;

        public VerifyEmailOTPCommandHandler(IOTPRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        public async Task<ServiceResponse<VerifyEmailOTPResponse>> Handle(VerifyEmailOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Find OTP by code and check if it's still valid
                var validOtp = await _otpRepository.GetItemWithCondition(
                    x => x.Code == request.Otp && 
                         !x.IsUsed && 
                         x.ExpirationTime > DateTime.UtcNow.AddHours(7), 
                    null, false);

                if (validOtp == null)
                {
                    return new ServiceResponse<VerifyEmailOTPResponse>
                    {
                        Data = new VerifyEmailOTPResponse
                        {
                            Verified = false,
                            Token = null
                        },
                        Success = false,
                        StatusCode = 400,
                        Message = "OTP không hợp lệ hoặc đã hết hạn."
                    };
                }

                // Mark OTP as used
                validOtp.IsUsed = true;
                await _otpRepository.Save();

                // Generate temporary token for registration
                var tempToken = GenerateTemporaryToken(request.Email);

                return new ServiceResponse<VerifyEmailOTPResponse>
                {
                    Data = new VerifyEmailOTPResponse
                    {
                        Verified = true,
                        Token = tempToken
                    },
                    Success = true,
                    StatusCode = 200,
                    Message = "OTP verified successfully."
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GenerateTemporaryToken(string email)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var data = $"{email}:{timestamp}";
            var bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }
    }
}
