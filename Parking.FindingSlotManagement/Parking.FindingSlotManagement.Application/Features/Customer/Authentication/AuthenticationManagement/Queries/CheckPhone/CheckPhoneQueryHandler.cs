using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.AuthenticationManagement.Queries.CheckPhone
{
    public class CheckPhoneQueryHandler : IRequestHandler<CheckPhoneQuery, ServiceResponse<CheckPhoneResponse>>
    {
        private readonly IUserRepository _userRepository;

        public CheckPhoneQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<CheckPhoneResponse>> Handle(CheckPhoneQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var phoneNumber = request.PhoneNumber;
                
                // Convert phone number format if needed (remove +84 and replace with 0)
                if (phoneNumber.StartsWith("+84"))
                {
                    phoneNumber = "0" + phoneNumber.Substring(3);
                }
                
                // Check if phone number exists in database for customers (RoleId = 3)
                var existingUser = await _userRepository.GetItemWithCondition(
                    x => x.Phone.Equals(phoneNumber) && x.RoleId == 3, 
                    null, true);
                
                bool phoneExists = existingUser != null;
                
                return new ServiceResponse<CheckPhoneResponse>
                {
                    Data = new CheckPhoneResponse
                    {
                        Exists = phoneExists
                    },
                    Success = true,
                    StatusCode = 200,
                    Message = phoneExists ? "Phone number exists" : "Phone number available"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<CheckPhoneResponse>
                {
                    Data = null,
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
}
