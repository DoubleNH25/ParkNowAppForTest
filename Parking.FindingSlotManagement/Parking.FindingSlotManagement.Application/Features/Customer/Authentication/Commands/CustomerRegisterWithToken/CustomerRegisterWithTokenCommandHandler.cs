using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.Commands.CustomerRegisterWithToken
{
    public class CustomerRegisterWithTokenCommandHandler : IRequestHandler<CustomerRegisterWithTokenCommand, ServiceResponse<CustomerRegisterWithTokenResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public CustomerRegisterWithTokenCommandHandler(IUserRepository userRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        public async Task<ServiceResponse<CustomerRegisterWithTokenResponse>> Handle(CustomerRegisterWithTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Verify token
                if (!IsValidToken(request.Token, request.Email))
                {
                    return new ServiceResponse<CustomerRegisterWithTokenResponse>
                    {
                        Data = new CustomerRegisterWithTokenResponse
                        {
                            Success = false,
                            Message = "Invalid or expired token."
                        },
                        Success = false,
                        StatusCode = 400,
                        Message = "Invalid or expired token."
                    };
                }

                // Check if phone already exists
                var existingUser = await _userRepository.GetItemWithCondition(x => x.Phone.Equals(request.PhoneNumber) && x.RoleId == 3, null, true);
                if (existingUser != null)
                {
                    return new ServiceResponse<CustomerRegisterWithTokenResponse>
                    {
                        Data = new CustomerRegisterWithTokenResponse
                        {
                            Success = false,
                            Message = "Phone number already registered."
                        },
                        Success = false,
                        StatusCode = 400,
                        Message = "Số điện thoại đã được đăng kí."
                    };
                }

                // Check if email already exists
                var existingEmailUser = await _userRepository.GetItemWithCondition(x => x.Email.Equals(request.Email) && x.RoleId == 3, null, true);
                if (existingEmailUser != null)
                {
                    return new ServiceResponse<CustomerRegisterWithTokenResponse>
                    {
                        Data = new CustomerRegisterWithTokenResponse
                        {
                            Success = false,
                            Message = "Email already registered."
                        },
                        Success = false,
                        StatusCode = 400,
                        Message = "Email đã được đăng kí."
                    };
                }

                // Create password hash
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                // Convert phone number format if needed (remove +84 and replace with 0)
                var phoneNumber = request.PhoneNumber;
                if (phoneNumber.StartsWith("+84"))
                {
                    phoneNumber = "0" + phoneNumber.Substring(3);
                }

                // Extract name from email (part before @)
                var userName = request.Email.Split('@')[0];

                // Create user
                var newUser = new User
                {
                    Phone = phoneNumber,
                    Email = request.Email,
                    Name = userName, // Extract from email
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsActive = true,
                    IsCensorship = true,
                    RoleId = 3, // Customer role
                    BanCount = 0
                };

                await _userRepository.Insert(newUser);

                // Create wallet for user
                var newWallet = new Domain.Entities.Wallet
                {
                    Balance = 0.0M,
                    Debt = 0.0M,
                    UserId = newUser.UserId
                };
                await _walletRepository.Insert(newWallet);

                return new ServiceResponse<CustomerRegisterWithTokenResponse>
                {
                    Data = new CustomerRegisterWithTokenResponse
                    {
                        Success = true,
                        UserId = newUser.UserId.ToString(),
                        Message = "Account created successfully."
                    },
                    Success = true,
                    StatusCode = 201,
                    Message = "Account created successfully."
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool IsValidToken(string token, string email)
        {
            try
            {
                var bytes = Convert.FromBase64String(token);
                var data = Encoding.UTF8.GetString(bytes);
                var parts = data.Split(':');
                
                if (parts.Length != 2) return false;
                
                var tokenEmail = parts[0];
                var timestamp = long.Parse(parts[1]);
                
                // Check email matches
                if (tokenEmail != email) return false;
                
                // Check token is not older than 10 minutes
                var tokenTime = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                var now = DateTimeOffset.UtcNow;
                
                return (now - tokenTime).TotalMinutes <= 10;
            }
            catch
            {
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
