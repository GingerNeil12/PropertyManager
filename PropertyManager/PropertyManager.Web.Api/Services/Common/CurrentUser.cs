using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PropertyManager.Application.Common.Interfaces;

namespace PropertyManager.Web.Api.Services.Common
{
    public class CurrentUser : ICurrentUser
    {
        public string UserId { get; private set; }

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            var identity = httpContext.HttpContext?.User?.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var idClaim = identity.Claims
                    .Where(x => x.Type.Equals(ClaimTypes.NameIdentifier))
                    .FirstOrDefault();

                UserId = idClaim?.Value;
            }
        }
    }
}
