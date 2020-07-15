using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PropertyManager.Domain.Enums;
using PropertyManager.Infrastructure.Security.Common;
using PropertyManager.ViewModels.Application.Landlords.Commands;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity;
using PropertyManager.Web.UI.Extensions;

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
        public async Task<IActionResult> LoadIndexData(string approvalStatus)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();

                var request = new GetLandlordsRequest()
                {
                    UserId = GetUserId(),
                    ApprovalStatus = (ApprovalStatus)Enum.Parse(typeof(ApprovalStatus), approvalStatus),
                    Filters = GetFilterDto()
                };
                var content = CreateContent(request);
                HttpClient.DefaultRequestHeaders.Authorization = GetAuthHeader();
                var url = $"{Configuration["Url:UserLandlords"]}/{GetUserId()}";
                var response = await HttpClient.PostAsync(url, content);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var okResponse = await response.GetOkResponseAsync();
                        var data = Deserialize<UserLandlordsViewModel>(okResponse.ResultString);
                        return Json(new
                        {
                            draw,
                            recordsFiltered = data.TotalRecords,
                            recordsTotal = data.TotalRecords,
                            data = data.Landlords
                        });

                    case HttpStatusCode.InternalServerError:
                    default:
                        return Json(new
                        {
                            draw,
                            recordsFiltered = 0,
                            recordsTotal = 0,
                            data = GetEmptyUserLandlordsViewModel().Landlords
                        });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = GetAuthHeader();
                var response = await HttpClient.GetAsync(
                    $"{Configuration["Url:DetailLandlord"]}/{id}");
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var okResponse = await response.GetOkResponseAsync();
                        var landlordDetailViewModel = Deserialize<LandlordDetailViewModel>(
                            okResponse.ResultString);
                        return View(landlordDetailViewModel);

                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.InternalServerError:
                    default:
                        return RedirectToAction("index", "landlord");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> LoadDetailsData(
            string landlordId,
            string dataType)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                switch (dataType)
                {
                    case "activities":
                        var activities = await GetLandlordActivities(landlordId);
                        return Json(new
                        {
                            draw,
                            recordsFiltered = activities.TotalRecords,
                            recordsTotal = activities.TotalRecords,
                            data = activities.Activity
                        });
                    default:
                        return Json(new
                        {
                            draw,
                            recordsFiltered = 0,
                            recordsTotal = 0
                        });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<LandlordActivityViewModel> GetLandlordActivities(
            string landlordId)
        {
            var request = new GetLandlordActivityRequest()
            {
                LandlordId = landlordId,
                Filter = GetFilterDto()
            };
            var content = CreateContent(request);
            HttpClient.DefaultRequestHeaders.Authorization = GetAuthHeader();
            var response = await HttpClient.PostAsync(
                Configuration["Url:LandlordActivities"],
                content);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var okResponse = await response.GetOkResponseAsync();
                    var result = Deserialize<LandlordActivityViewModel>(okResponse.ResultString);
                    return result;

                default:
                    return new LandlordActivityViewModel()
                    {
                        TotalRecords = 0,
                        Activity = new List<LandlordActivityDto>()
                    };
            }
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
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Created:
                            var createdResult = await response.GetCreatedResponseAsync();
                            return RedirectToAction("Details", new { id = createdResult.IdString });

                        case HttpStatusCode.BadRequest:
                            var badRequestResult = await response.GetBadRequestResponseAsync();
                            AddBadRequestErrorsToModelState(badRequestResult);
                            return View(request);

                        case HttpStatusCode.Unauthorized:
                            var unauthorizedResponse = await response.GetUnauthorizedResponseAsync();
                            return View(request);

                        case HttpStatusCode.Forbidden:
                            var forbiddenResponse = await response.GetForbiddenResponseAsync();
                            return View(request);

                        case HttpStatusCode.InternalServerError:
                            var internalServerResult = await response.GetInternalServerErrorResponseAsync();
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

        private UserLandlordsViewModel GetEmptyUserLandlordsViewModel()
        {
            return new UserLandlordsViewModel()
            {
                Landlords = new List<LandlordsDto>(),
                TotalRecords = 0
            };
        }
    }
}
