using MediatR;
using PropertyManager.ViewModels.Application.Common;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity
{
    public class GetLandlordActivityRequest : IRequest<LandlordActivityViewModel>
    {
        public string LandlordId { get; set; }
        public FilterDto Filter { get; set; }
    }
}
