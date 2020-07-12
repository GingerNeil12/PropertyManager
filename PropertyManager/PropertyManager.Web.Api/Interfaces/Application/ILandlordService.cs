using System.Threading.Tasks;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Landlords.Commands;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity;

namespace PropertyManager.Web.Api.Interfaces.Application
{
    public interface ILandlordService
    {
        Task<ResponseMessage> CreateLandlordAsync(CreateLandlordRequest request);
        Task<ResponseMessage> GetLandlordsForUserAsync(GetLandlordsRequest request);
        Task<ResponseMessage> GetLandlordDetailsAsync(GetLandlordDetailRequest request);
        Task<ResponseMessage> GetLandlordActivitiesAsync(GetLandlordActivityRequest request);
    }
}
