using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Account.AccountManagement.Commands.ChangeCustomerPassword
{
    public class ChangeCustomerPasswordCommandHandler : IRequestHandler<ChangeCustomerPasswordCommand, ServiceResponse<string>>
    {
        private readonly IUserRepository _userRepository;

        public ChangeCustomerPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<string>> Handle(ChangeCustomerPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if user exists
                var user = await _userRepository.GetById(request.UserId);
                if (user == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Không tìm thấy tài khoản.",
                        StatusCode = 404,
                        Success = false
                    };
                }

                // Verify current password
                if (!VerifyPasswordHash(request.CurrentPassword, user.PasswordHash, user.PasswordSalt))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Mật khẩu hiện tại không đúng.",
                        StatusCode = 400,
                        Success = false
                    };
                }

                // Check if new password is same as current password
                if (VerifyPasswordHash(request.NewPassword, user.PasswordHash, user.PasswordSalt))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Mật khẩu mới không được trùng với mật khẩu hiện tại.",
                        StatusCode = 400,
                        Success = false
                    };
                }

                // Create new password hash
                CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                
                // Update user password
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                
                await _userRepository.Update(user);

                return new ServiceResponse<string>
                {
                    Message = "Thành công",
                    StatusCode = 204,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash == null || storedSalt == null)
                return false;

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
