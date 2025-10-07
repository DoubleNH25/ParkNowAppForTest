using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Parking.FindingSlotManagement.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            if (httpContext == null) throw new UnauthorizedAccessException("Missing HTTP context");
            var user = httpContext.User;
            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var claim = user.FindFirst(ClaimTypes.NameIdentifier)
                       ?? user.FindFirst("nameid")
                       ?? user.FindFirst("sub")
                       ?? user.FindFirst("uid")
                       ?? user.FindFirst(ClaimTypes.Sid)
                       ?? user.FindFirst("UserId")
                       ?? user.FindFirst("userId")
                       ?? user.FindFirst("_id")
                       ?? user.FindFirst("id");

            if (claim == null)
            {
                throw new UnauthorizedAccessException("UserId claim not found in token");
            }

            if (!int.TryParse(claim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid UserId claim value");
            }

            return userId;
        }
    }
}
