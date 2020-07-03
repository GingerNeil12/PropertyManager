using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using PropertyManager.Web.UI.Common;

namespace PropertyManager.Web.UI.Controllers
{
    [Authorize]
    public class AuthController : BaseController
    {
        protected AuthController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        protected AuthenticationHeaderValue GetAuthHeader()
        {
            var accessTokenClaim = GetClaimsFromUser()
                .Where(x => x.Type.Equals(ProjectConstants.ACCESS_TOKEN_CLAIM))
                .FirstOrDefault();

            return new AuthenticationHeaderValue(
                ProjectConstants.BEARER,
                accessTokenClaim.Value);
        }

        protected string GetUserId()
        {
            var idClaim = GetClaimsFromUser()
                .Where(x => x.Type.Equals(ClaimTypes.NameIdentifier))
                .FirstOrDefault();

            return idClaim.Value;
        }

        private IEnumerable<Claim> GetClaimsFromUser()
        {
            var identity = User.Identity as ClaimsIdentity;
            return identity.Claims;
        }
    }
}
