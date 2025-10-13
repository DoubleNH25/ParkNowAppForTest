using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.FindingSlotManagement.Application;
using Parking.FindingSlotManagement.Application.Features.Customer.Parking.Queries.GetParkingDetails;
using Parking.FindingSlotManagement.Application.Features.Customer.Parking.Queries.GetAllParkingWithActiveStatus;
using Parking.FindingSlotManagement.Infrastructure.Hubs;

namespace Parking.FindingSlotManagement.Api.Controllers.Customer
{
    [Route("api/parkings")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParkingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("mobile/parking-details")]
        public async Task<ActionResult<ServiceResponse<GetParkingDetailsResponse>>> GetParkingDetails(
            [FromQuery] GetParkingDetailsQuery query)
        {
            try
            {
                var res = await _mediator.Send(query);
                if (res.Message == "Thành công")
                {
                    return StatusCode((int)res.StatusCode, res);
                }
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("mobile/all-active")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAllParkingWithActiveStatusResponse>>>> GetAllActiveParking(
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = new GetAllParkingWithActiveStatusQuery { PageNo = pageNo, PageSize = pageSize };
                var res = await _mediator.Send(query);
                
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}