using Microsoft.AspNetCore.Mvc;
using PropertyManager.Web.Api.FIlters;

namespace PropertyManager.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [LoggingFilter]
        public IActionResult Index()
        {
            return Ok("PROPERTY MANAGER API HOME");
        }
    }
}
