using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyOTPForRegistration
{
    public class VerifyOTPForRegistrationCommandHandler : IRequestHandler<VerifyOTPForRegistrationCommand, ServiceResponse<VerifyOTPForRegistrationResponse>>
    {
        private readonly IMemoryCache _memoryCache;

        public VerifyOTPForRegistrationCommandHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<ServiceResponse<VerifyOTPForRegistrationResponse>> Handle(VerifyOTPForRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get OTP from memory cache
                var cacheKey = $"registration_otp_{request.Email}";
                var cachedOtp = _memoryCache.Get<string>(cacheKey);

                if (cachedOtp == null || cachedOtp != request.OtpCode)
                {
                    return new ServiceResponse<VerifyOTPForRegistrationResponse>
                    {
                        Data = new VerifyOTPForRegistrationResponse
                        {
                            Verified = false,
                            Token = null
                        },
                        Success = false,
                        StatusCode = 400,
                        Message = "OTP không hợp lệ hoặc đã hết hạn."
                    };
                }

                // Remove OTP from cache after successful verification
                _memoryCache.Remove(cacheKey);

                // Generate temporary token for registration
                var tempToken = GenerateTemporaryToken(request.Email);

                return new ServiceResponse<VerifyOTPForRegistrationResponse>
                {
                    Data = new VerifyOTPForRegistrationResponse
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
