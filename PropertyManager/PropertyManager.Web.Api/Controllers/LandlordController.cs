using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Infrastructure.Security.Common;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Landlords.Commands;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;
using PropertyManager.Web.Api.Interfaces.Application;

namespace PropertyManager.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LandlordController : AuthController
    {
        private readonly ILandlordService _landlordService;

        public LandlordController(ILandlordService landlordService)
        {
            _landlordService = landlordService;
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> CreateLandlordAsync(
            [FromBody] CreateLandlordRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _landlordService.CreateLandlordAsync(request);
                return StatusCode(result.Status, result.Payload);
            }
            return GetBadRequestResult();
        }

        [HttpPost]
        [Route("user/{userId}")]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> GetLandlordsForUserAsync(
            [FromRoute] string userId,
            [FromBody] GetLandlordsRequest request)
        {
            if (userId != request.UserId)
            {
                var forbiddenResult = new ForbiddenApiResponse();
                return StatusCode(forbiddenResult.Status, forbiddenResult);
            }

            if (ModelState.IsValid)
            {
                var result = await _landlordService.GetLandlordsForUserAsync(request);
                return StatusCode(result.Status, result.Payload);
            }
            return GetBadRequestResult();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> GetLandlordDetailsAsync(string id)
        {
            if (ModelState.IsValid)
            {
                var request = new GetLandlordDetailRequest() { LandlordId = id };
                var result = await _landlordService.GetLandlordDetailsAsync(request);
                return StatusCode(result.Status, result.Payload);
            }
            return GetBadRequestResult();
        }
    }
}
