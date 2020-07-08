using MediatR;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;

namespace PropertyManager.Application.Landlords.Queries.GetLandlords
{
    public class GetLandlordsRequest : IRequest<LandlordsViewModel>
    {
        public string UserId { get; set; }
    }
}
