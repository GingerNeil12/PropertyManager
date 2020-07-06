using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Landlords.Commands;
using PropertyManager.Web.Api.Interfaces.Application;

namespace PropertyManager.Web.Api.Services.Application
{
    public class LandlordService : BaseService, ILandlordService
    {
        public LandlordService(IHttpContextAccessor httpContext)
            : base(httpContext)
        {
        }

        public async Task<ResponseMessage> CreateLandlordAsync(
            CreateLandlordRequest request)
        {
            try
            {
                var result = await Mediator.Send(request);
                return CreatedResponse(result, "Landlord Created.");
            }
            catch (Exception ex)
            {
                return GetResponseMessageForException(ex);
            }
        }
    }
}
