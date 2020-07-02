using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Security;
using PropertyManager.Web.Api.Extensions;
using PropertyManager.Web.Api.FIlters;
using PropertyManager.Web.Api.Interfaces.Security;

namespace PropertyManager.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        [LoggingFilter]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.AuthenticateUserAsync(model);
                return StatusCode(result.Status, result.Payload);
            }
            return new BadRequestObjectResult(
                new BadRequestApiResponse(
                    ModelState.ConvertToDictionary()));
        }
    }
}
