using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Infrastructure;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.SendMailWithOTP
{
    public class SendMailWithOTPCommandsHandler : IRequestHandler<SendMailWithOTPCommands, ServiceResponse<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public SendMailWithOTPCommandsHandler(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }
        public async Task<ServiceResponse<string>> Handle(SendMailWithOTPCommands request, CancellationToken cancellationToken)
        {
            try
            {
                // For registration flow with phone number - allow if no existing user
                if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    // Generate OTP automatically if not provided
                    var otpCode = request.OTP ?? GenerateOtp();
                    
                    EmailModel emailModel = new EmailModel();
                    emailModel.To = request.Email;
                    emailModel.Subject = "Your OTP code for Registration";
                    emailModel.Body = $"Your OTP code is {otpCode}. It will expire in 5 minutes.";
                    await _emailService.SendMail(emailModel);
                    
                    return new ServiceResponse<string>
                    {
                        Message = "OTP has been sent to your email.",
                        Success = true,
                        StatusCode = 201
                    };
                }
                
                // Original flow - check existing user
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
                EmailModel emailModelExisting = new EmailModel();
                emailModelExisting.To = checkUserExist.Email;
                emailModelExisting.Subject = "Your OTP code";
                emailModelExisting.Body = $"Your OTP code is {request.OTP}.";
                await _emailService.SendMail(emailModelExisting);
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
    }
}
