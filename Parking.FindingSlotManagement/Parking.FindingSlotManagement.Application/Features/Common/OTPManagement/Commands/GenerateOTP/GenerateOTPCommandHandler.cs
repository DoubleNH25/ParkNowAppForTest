using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Infrastructure;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Models;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.GenerateOTP
{
    public class GenerateOTPCommandHandler : IRequestHandler<GenerateOTPCommand, ServiceResponse<string>>
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public GenerateOTPCommandHandler(IOTPRepository otpRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task<ServiceResponse<string>> Handle(GenerateOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // For registration flow
                if (request.IsForRegistration)
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

                    // Generate OTP for registration (temporary user with ID = 0)
                    var registrationOtp = GenerateOtp();
                    var registrationOtpExpirationTime = GetOtpExpirationTime();
                    
                    var registrationOtpEntity = new OTP
                    {
                        Code = registrationOtp,
                        ExpirationTime = registrationOtpExpirationTime,
                        UserId = 0, // Temporary placeholder for registration
                        CreatedDate = DateTime.UtcNow.AddHours(7),
                        IsUsed = false
                    };
                    await _otpRepository.Insert(registrationOtpEntity);
                    
                    EmailModel registrationEmailModel = new EmailModel();
                    registrationEmailModel.To = request.Email;
                    registrationEmailModel.Subject = "Your OTP code for Registration";
                    registrationEmailModel.Body = $"Your OTP code is {registrationOtp}. It will expire in 5 minutes.";
                    await _emailService.SendMail(registrationEmailModel);
                    
                    return new ServiceResponse<string>
                    {
                        Message = "OTP has been sent to your email.",
                        Success = true,
                        StatusCode = 201
                    };
                }

                // Original flow for existing users
                var checkUserExist = await _userRepository.GetItemWithCondition(x => x.Email.Equals(request.Email) && x.IsActive == true && x.IsCensorship == true, null, true);
                if (checkUserExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Không tìm thấy tài khoản hoặc tài khoản đã bị ban.",
                        StatusCode = 200,
                        Success = true
                    };
                }
                var checkOtpExistFromThisAccount = await _otpRepository.GetItemWithCondition(x => x.UserId == checkUserExist.UserId && !x.IsUsed, null, false);
                if(checkOtpExistFromThisAccount != null)
                {
                    await _otpRepository.Delete(checkOtpExistFromThisAccount);
                }
                var existingUserOtpExpirationTime = GetOtpExpirationTime();
                var existingUserOtp = GenerateOtp();
                var existingUserOtpEntity = new OTP
                {
                    Code = existingUserOtp,
                    ExpirationTime = existingUserOtpExpirationTime,
                    UserId = checkUserExist.UserId,
                    CreatedDate = DateTime.UtcNow.AddHours(7),
                    IsUsed = false
                };
                await _otpRepository.Insert(existingUserOtpEntity);
                EmailModel existingUserEmailModel = new EmailModel();
                existingUserEmailModel.To = checkUserExist.Email;
                existingUserEmailModel.Subject = "Your OTP code";
                existingUserEmailModel.Body = $"Your OTP code is {existingUserOtp}. It will expire in 5 minutes.";
                await _emailService.SendMail(existingUserEmailModel);
                return new ServiceResponse<string>
                {
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
