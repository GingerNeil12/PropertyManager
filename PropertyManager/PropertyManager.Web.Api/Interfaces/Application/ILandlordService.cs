using System.Threading.Tasks;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Landlords.Commands;

namespace PropertyManager.Web.Api.Interfaces.Application
{
    public interface ILandlordService
    {
        Task<ResponseMessage> CreateLandlordAsync(CreateLandlordRequest request);
    }
}
