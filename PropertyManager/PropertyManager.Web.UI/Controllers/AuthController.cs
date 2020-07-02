using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

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
    }
}
