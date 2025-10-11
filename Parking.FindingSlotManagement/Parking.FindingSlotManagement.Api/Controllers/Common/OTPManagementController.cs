using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.FindingSlotManagement.Application;
using Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.GenerateOTP;
using Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.SendMailWithOTP;
using Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyOTP;
using Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyOTPForRegistration;
using System.Net;

namespace Parking.FindingSlotManagement.Api.Controllers.Common
{
    [Route("api/otp-management")]
    [ApiController]
    public class OTPManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OTPManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("send-email-otp", Name = "SendEmailOTP")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> SendEmailOTP([FromBody] SendMailWithOTPCommands command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (res.Message != "Thành công")
                {
                    return StatusCode((int)res.StatusCode, res);
                }
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                IEnumerable<string> list1 = new List<string> { "Severity: Error" };
                string message = "";
                foreach (var item in list1)
                {
                    message = ex.Message.Replace(item, string.Empty);
                }
                var errorResponse = new ErrorResponseModel(ResponseCode.BadRequest, "Validation Error: " + message.Remove(0, 31));
                return StatusCode((int)ResponseCode.BadRequest, errorResponse);
            }
        }
        [HttpPost("verify", Name = "VerifyOTP")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> VerifyOTP([FromBody] VerifyOTPCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (res.Message != "Thành công")
                {
                    return StatusCode((int)res.StatusCode, res);
                }
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                IEnumerable<string> list1 = new List<string> { "Severity: Error" };
                string message = "";
                foreach (var item in list1)
                {
                    message = ex.Message.Replace(item, string.Empty);
                }
                var errorResponse = new ErrorResponseModel(ResponseCode.BadRequest, "Validation Error: " + message.Remove(0, 31));
                return StatusCode((int)ResponseCode.BadRequest, errorResponse);
            }
        }

        [HttpPost("generate", Name = "GenerateOTP")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> GenerateOTP([FromBody] GenerateOTPCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (res.Message != "Thành công" && res.Message != "OTP has been sent to your email.")
                {
                    return StatusCode((int)res.StatusCode, res);
                }
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                IEnumerable<string> list1 = new List<string> { "Severity: Error" };
                string message = "";
                foreach (var item in list1)
                {
                    message = ex.Message.Replace(item, string.Empty);
                }
                var errorResponse = new ErrorResponseModel(ResponseCode.BadRequest, "Validation Error: " + message.Remove(0, 31));
                return StatusCode((int)ResponseCode.BadRequest, errorResponse);
            }
        }

        [HttpPost("verify-registration", Name = "VerifyRegistrationOTP")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<VerifyOTPForRegistrationResponse>>> VerifyRegistrationOTP([FromBody] VerifyOTPForRegistrationCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponseModel(ResponseCode.BadRequest, "Validation Error: " + ex.Message);
                return StatusCode((int)ResponseCode.BadRequest, errorResponse);
            }
        }
    }
}
