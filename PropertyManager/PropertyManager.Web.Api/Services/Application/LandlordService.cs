using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Landlords.Commands;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity;
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

        public async Task<ResponseMessage> GetLandlordActivitiesAsync(
            GetLandlordActivityRequest request)
        {
            try
            {
                var result = await Mediator.Send(request);
                return OkResponse(result);
            }
            catch (Exception ex)
            {
                return GetResponseMessageForException(ex);
            }
        }

        public async Task<ResponseMessage> GetLandlordDetailsAsync(
            GetLandlordDetailRequest request)
        {
            try
            {
                var result = await Mediator.Send(request);
                return OkResponse(result);
            }
            catch (Exception ex)
            {
                return GetResponseMessageForException(ex);
            }
        }

        public async Task<ResponseMessage> GetLandlordsForUserAsync(
            GetLandlordsRequest request)
        {
            try
            {
                var result = await Mediator.Send(request);
                return OkResponse(result);
            }
            catch (Exception ex)
            {
                return GetResponseMessageForException(ex);
            }
        }
    }
}
