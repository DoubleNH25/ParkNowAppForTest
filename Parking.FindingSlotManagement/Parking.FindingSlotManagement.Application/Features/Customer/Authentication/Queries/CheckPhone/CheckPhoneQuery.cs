using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Authentication.Queries.CheckPhone
{
    public class CheckPhoneQuery : IRequest<ServiceResponse<CheckPhoneResponse>>
    {
        public string PhoneNumber { get; set; }
    }
}
