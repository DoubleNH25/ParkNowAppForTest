using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.FindingSlotManagement.Application.Features.Customer.Wallet.Queries.GetWalletByUserId;
using Parking.FindingSlotManagement.Application;
using Parking.FindingSlotManagement.Api.Extensions;
using System.Net;
using Parking.FindingSlotManagement.Application.Features.Customer.Transaction.Queries.GetAllTransactionByUserId;

namespace Parking.FindingSlotManagement.Api.Controllers.Customer
{
    [Route("api/customer/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// API For Customer, Manager
        /// </summary>
        /// 
        [Authorize(Roles = "Customer,Manager")]
        [HttpGet(Name = "GetAllTransactionByUserId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAllTransactionByUserIdResponse>>>> GetAllTransactionByUserId()
        {
            try
            {
                var userId = HttpContext.GetUserId();
                var query = new GetAllTransactionByUserIdQuery() { UserId = userId };
                var res = await _mediator.Send(query);
                if (res.Message != "Thành công")
                {
                    return StatusCode((int)res.StatusCode, res);
                }
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
