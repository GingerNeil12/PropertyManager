using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PropertyManager.Infrastructure.Security.Common;

namespace PropertyManager.Web.Api.Controllers
{
    [Authorize(AuthenticationSchemes = AuthSchemes.BEARER)]
    public abstract class AuthController : BaseController
    {
        protected string GetUserId()
        {
            var identity = User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims
                .Where(x => x.Type.Equals(ClaimTypes.NameIdentifier))
                .FirstOrDefault();
            return idClaim.Value;
        }
    }
}
