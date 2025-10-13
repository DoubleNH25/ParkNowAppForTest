using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Parking.Queries.GetAllParkingWithActiveStatus
{
    public class GetAllParkingWithActiveStatusQuery : IRequest<ServiceResponse<IEnumerable<GetAllParkingWithActiveStatusResponse>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}