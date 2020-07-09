using System;
using System.Collections.Generic;
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
        private const string UNABLE_TO_LOAD_LANDLORDS = "Unable to load landlords. Try again later.";

        private const int PAGE_SIZE = 10;

        public LandlordController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var request = new GetLandlordsRequest()
                {
                    PageSize = PAGE_SIZE,
                    UserId = GetUserId()
                };
                var content = CreateContent(request);
                HttpClient.DefaultRequestHeaders.Authorization = GetAuthHeader();
                var url = $"{Configuration["Url:UserLandlords"]}/{GetUserId()}";
                var response = await HttpClient.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                switch(response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var okResponse = Deserialize<OkApiResponse>(responseBody);
                        var landlordsViewModel = Deserialize<LandlordsViewModel>(
                            okResponse.Result.ToString());
                        return View(landlordsViewModel);
                    case HttpStatusCode.Unauthorized:
                        return View(CreateEmptyLandlordsVM());
                    case HttpStatusCode.Forbidden:
                        return View(CreateEmptyLandlordsVM());
                    case HttpStatusCode.InternalServerError:
                    default:
                        ViewData["Message"] = UNABLE_TO_LOAD_LANDLORDS;
                        return View(CreateEmptyLandlordsVM());
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = UNABLE_TO_LOAD_LANDLORDS;
                return View(CreateEmptyLandlordsVM());
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
    
        private LandlordsViewModel CreateEmptyLandlordsVM()
        {
            var filterDto = new FilterDto()
            {
                ApprovalStatus = new ApprovalStatusDto(),
                ApprovalStatuses = new List<ApprovalStatusDto>()
            };
            var landlords = new List<LandlordsDto>();
            return new LandlordsViewModel()
            {
                Filter = filterDto,
                Landlords = landlords,
                PageNumber = 0,
                PageSize = 0,
                TotalPages = 0,
                TotalRecords = 0
            };
        }
    }
}
