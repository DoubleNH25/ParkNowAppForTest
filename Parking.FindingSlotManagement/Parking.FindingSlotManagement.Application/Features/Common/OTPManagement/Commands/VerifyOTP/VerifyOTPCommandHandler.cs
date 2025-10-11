using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Infrastructure;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyOTP
{
    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, ServiceResponse<string>>
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IUserRepository _userRepository;

        public VerifyOTPCommandHandler(IOTPRepository otpRepository, IUserRepository userRepository)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
        }
        public async Task<ServiceResponse<string>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
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
                var otpExist = await _otpRepository.GetItemWithCondition(x => x.UserId == checkUserExist.UserId && !x.IsUsed, null, false);
                if(otpExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Không tìm thấy OTP hoặc OTP đã được sử dụng.",
                        Success = false,
                        StatusCode = 400
                    };
                }
                if(otpExist.Code != request.OtpCode)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "OTP không hợp lệ.",
                        StatusCode = 400,
                        Success = false
                    };
                }
                if(otpExist.ExpirationTime <= DateTime.UtcNow.AddHours(7))
                {
                    otpExist.IsUsed = true;
                    await _otpRepository.Save();
                    return new ServiceResponse<string>
                    {
                        Message = "OTP đã hết hạn.",
                        StatusCode = 400,
                        Success = false
                    };
                }
                // Mark OTP as used instead of deleting
                otpExist.IsUsed = true;
                await _otpRepository.Save();
                
                return new ServiceResponse<string>
                {
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
