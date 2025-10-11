using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Parking.FindingSlotManagement.Application;
using Parking.FindingSlotManagement.Application.Features.Customer.Authentication.AuthenticationManagement.Queries.CustomerLogin;
using Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.SendMailWithOTP;
using Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.GenerateOTPForRegistration;
using Parking.FindingSlotManagement.Application.Features.Common.OTPManagement.Commands.VerifyOTPForRegistration;
using Parking.FindingSlotManagement.Application.Features.Customer.Authentication.Commands.CustomerRegisterWithToken;
using Parking.FindingSlotManagement.Application.Features.Customer.Authentication.AuthenticationManagement.Queries.CheckPhone;
using Parking.FindingSlotManagement.Infrastructure.Hubs;
using Parking.FindingSlotManagement.Api.Models;
using System.Net;

namespace Parking.FindingSlotManagement.Api.Controllers.Customer
{
    [Route("api/mobile/customer-authentication")]
    [ApiController]
    public class AuthenticationCustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<MessageHub> _messageHub;

        public AuthenticationCustomerController(IMediator mediator, IHubContext<MessageHub> messageHub)
        {
            _mediator = mediator;
            _messageHub = messageHub;
        }
        /// <summary>
        /// API for Customer
        /// </summary>
        [HttpPost("login", Name ="CustomerLogin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<string>>> CustomerLogin(CustomerLoginQuery request)
        {
            try
            {
                var res = await _mediator.Send(request);
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
        /// <summary>
        /// Step 1: Check if phone number exists
        /// </summary>
        [HttpPost("check-phone", Name = "CheckPhoneForRegistration")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<CheckPhoneResponse>>> CheckPhone([FromBody] CheckPhoneRequest request)
        {
            try
            {
                var query = new CheckPhoneQuery
                {
                    PhoneNumber = request.PhoneNumber
                };

                var res = await _mediator.Send(query);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Step 2: Send OTP to email for registration
        /// </summary>
        [HttpPost("send-email-otp", Name = "SendEmailOTPForRegistration")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> SendEmailOTP([FromBody] SendEmailOTPForRegistrationRequest request)
        {
            try
            {
                var command = new GenerateOTPForRegistrationCommand
                {
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };
                var res = await _mediator.Send(command);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Step 3: Verify email OTP and get temporary token  
        /// </summary>
        [HttpPost("verify-email-otp", Name = "VerifyEmailOTPForRegistration")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<VerifyOTPForRegistrationResponse>>> VerifyEmailOTP([FromBody] VerifyEmailOTPRequest request)
        {
            try
            {
                var command = new VerifyOTPForRegistrationCommand
                {
                    Email = request.Email,
                    OtpCode = request.Otp
                };
                var res = await _mediator.Send(command);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Step 4: Complete registration with token
        /// </summary>
        [HttpPost("register", Name = "CustomerRegisterWithTokenNew")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<CustomerRegisterWithTokenResponse>>> CustomerRegisterWithToken([FromBody] CustomerRegisterRequest request)
        {
            try
            {
                var command = new CustomerRegisterWithTokenCommand
                {
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Password = request.Password,
                    Token = request.Token
                };

                var res = await _mediator.Send(command);
                
                if (res.Success)
                {
                    await _messageHub.Clients.All.SendAsync("LoadCustomerAccountsInAdmin");
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
