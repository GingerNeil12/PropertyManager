using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Infrastructure.Security.Common;
using PropertyManager.ViewModels.Application.Landlords.Commands;
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
            return GetBadRequetsResult();
        }
    }
}
