using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PropertyManager.Infrastructure.Security.Common;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Landlords.Commands;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;

namespace PropertyManager.Web.UI.Controllers
{
    public class LandlordController : AuthController
    {
        private const string UNABLE_CREATE_LANDLORD = "Unable to create landlord. Try again later.";

        public LandlordController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ADMIN)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> LoadIndexData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();

                var request = new GetLandlordsRequest()
                {
                    UserId = GetUserId(),
                    Filters = CreateFilterDto()
                };
                var content = CreateContent(request);
                HttpClient.DefaultRequestHeaders.Authorization = GetAuthHeader();
                var url = $"{Configuration["Url:UserLandlords"]}/{GetUserId()}";
                var response = await HttpClient.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var okResponse = Deserialize<OkApiResponse>(responseBody);
                        var data = Deserialize<LandlordsViewModel>(okResponse.Result.ToString());
                        return Json(new
                        {
                            draw,
                            recordsFiltered = data.TotalRecords,
                            recordsTotal = data.TotalRecords,
                            data = data.Landlords
                        });
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ADMIN)]
        public IActionResult Details(string id)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ADMIN)]
        public IActionResult Create()
        {
            return View(new CreateLandlordRequest());
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> Create(CreateLandlordRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var content = CreateContent(request);
                    HttpClient.DefaultRequestHeaders.Authorization = GetAuthHeader();
                    var response = await HttpClient.PostAsync(
                        Configuration["Url:CreateLandlord"],
                        content);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Created:
                            var createdResult = Deserialize<CreatedApiResponse>(responseBody);
                            return RedirectToAction("Details", new { id = createdResult.Id.ToString() });
                        case HttpStatusCode.BadRequest:
                            var badRequestResult = Deserialize<BadRequestApiResponse>(responseBody);
                            AddBadRequestErrorsToModelState(badRequestResult);
                            return View(request);
                        case HttpStatusCode.Unauthorized:
                            return View(request);
                        case HttpStatusCode.Forbidden:
                            return View(request);
                        case HttpStatusCode.InternalServerError:
                            var internalServerResult = Deserialize<InternalServerErrorApiResponse>(responseBody);
                            ViewData["Message"] = internalServerResult.Status;
                            return View(request);
                        default:
                            ViewData["Message"] = UNABLE_CREATE_LANDLORD;
                            return View(request);
                    }
                }
                catch (Exception)
                {
                    ViewData["Message"] = UNABLE_CREATE_LANDLORD;
                    return View(request);
                }
            }
            return View(request);
        }

        private FilterDto CreateFilterDto()
        {
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var orderColumn = Request.Form["order[0][column]"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + orderColumn + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var skip = start != null ? Convert.ToInt32(start) : 0;
            var pageSize = length != null ? Convert.ToInt32(length) : 0;

            if (sortColumn == "LandlordId")
            {
                sortColumn = "LastName";
            }

            var result = new FilterDto()
            {
                Skip = skip,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortColumnDirection,
                SearchValue = searchValue
            };
            return result;
        }
    }
}
