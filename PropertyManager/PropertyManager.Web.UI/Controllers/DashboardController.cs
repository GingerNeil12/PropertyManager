using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PropertyManager.Web.UI.Controllers
{
    public class DashboardController : AuthController
    {
        public DashboardController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
