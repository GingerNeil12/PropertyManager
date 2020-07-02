using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Security;
using PropertyManager.Web.UI.Interfaces;

namespace PropertyManager.Web.UI.Controllers
{
    public class LoginController : BaseController
    {
        private const string DEFAULT_ERROR = "Unable to login. Try again later.";

        private readonly IAuthCookie _authCookie;

        public LoginController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IAuthCookie authCookie)
            : base(httpClientFactory, configuration)
        {
            _authCookie = authCookie;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            try
            {
                var content = CreateContent(model);
                var response = await HttpClient.PostAsync(Configuration["Url:Login"], content);
                var responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        await OkResponse(responseBody);
                        return RedirectToAction("Index", "Dashboard");
                    case HttpStatusCode.BadRequest:
                        BadRequestResponse(responseBody);
                        return View(model);
                    case HttpStatusCode.Unauthorized:
                        UnauthorizedResponse(responseBody);
                        return View(model);
                    default:
                        ViewData["Message"] = DEFAULT_ERROR;
                        return View(model);
                }
            }
            catch (Exception)
            {
                ViewData["Message"] = DEFAULT_ERROR;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authCookie.Destroy();
            return RedirectToAction("Index", "Login");
        }

        private async Task OkResponse(string responseBody)
        {
            var okResponse = Deserialize<OkApiResponse>(responseBody);
            await _authCookie.Create(okResponse.Result.ToString());
        }

        private void BadRequestResponse(string responseBody)
        {
            var badRequest = Deserialize<BadRequestApiResponse>(responseBody);
            AddBadRequestErrorsToModelState(badRequest);
        }

        private void UnauthorizedResponse(string responseBody)
        {
            var unauthorized = Deserialize<UnauthorizedApiResponse>(responseBody);
            ViewData["Message"] = unauthorized.Title;
        }
    }
}
