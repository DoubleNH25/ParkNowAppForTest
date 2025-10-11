using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.Queries.CheckPhone
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
                var existingUser = await _userRepository.GetItemWithCondition(x => x.Phone.Equals(request.PhoneNumber) && x.RoleId == 3, null, true);
                
                return new ServiceResponse<CheckPhoneResponse>
                {
                    Data = new CheckPhoneResponse
                    {
                        Exists = existingUser != null
                    },
                    Success = true,
                    StatusCode = 200,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
