using System.Threading.Tasks;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Landlords.Commands;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;

namespace PropertyManager.Web.Api.Interfaces.Application
{
    public interface ILandlordService
    {
        Task<ResponseMessage> CreateLandlordAsync(CreateLandlordRequest request);
        Task<ResponseMessage> GetLandlordsForUserAsync(GetLandlordsRequest request);
        Task<ResponseMessage> GetLandlordDetailsAsync(GetLandlordDetailRequest request);
    }
}
