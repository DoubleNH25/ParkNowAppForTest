using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Parking.FindingSlotManagement.Application.Contracts.Infrastructure;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Models;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.GenerateOTPForRegistration
{
    public class GenerateOTPForRegistrationCommandHandler : IRequestHandler<GenerateOTPForRegistrationCommand, ServiceResponse<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;

        public GenerateOTPForRegistrationCommandHandler(IUserRepository userRepository, IEmailService emailService, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _memoryCache = memoryCache;
        }

        public async Task<ServiceResponse<string>> Handle(GenerateOTPForRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if email or phone already exists
                var existingUser = await _userRepository.GetItemWithCondition(
                    x => (x.Email.Equals(request.Email) || x.Phone.Equals(request.PhoneNumber)) && x.RoleId == 3, 
                    null, true);
                
                if (existingUser != null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Email hoặc số điện thoại đã được đăng ký.",
                        StatusCode = 400,
                        Success = false
                    };
                }

                // Generate OTP and store in memory cache
                var otp = GenerateOtp();
                var cacheKey = $"registration_otp_{request.Email}";
                
                // Store OTP in memory cache for 5 minutes
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    Priority = CacheItemPriority.High
                };
                
                _memoryCache.Set(cacheKey, otp, cacheOptions);
                
                EmailModel emailModel = new EmailModel();
                emailModel.To = request.Email;
                emailModel.Subject = "Your OTP code for Registration";
                emailModel.Body = $"Your OTP code is {otp}. It will expire in 5 minutes.";
                await _emailService.SendMail(emailModel);
                
                return new ServiceResponse<string>
                {
                    Message = "OTP has been sent to your email.",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                // Log detailed error for debugging
                var innerException = ex.InnerException?.Message ?? "No inner exception";
                var fullError = $"Error: {ex.Message}. Inner: {innerException}";
                throw new Exception(fullError);
            }
        }

        private string GenerateOtp()
        {
            const string chars = "0123456789";
            var random = new Random();
            var otp = new StringBuilder(6);

            for (int i = 0; i < 6; i++)
            {
                otp.Append(chars[random.Next(chars.Length)]);
            }

            return otp.ToString();
        }

        private DateTime GetOtpExpirationTime()
        {
            return DateTime.UtcNow.AddHours(7).AddMinutes(5);
        }
    }
}
