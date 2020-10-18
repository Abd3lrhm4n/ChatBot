using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatbot.Utilities
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return null;
            }

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
