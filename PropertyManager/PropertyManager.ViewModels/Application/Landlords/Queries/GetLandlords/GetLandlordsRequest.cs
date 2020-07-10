using MediatR;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class GetLandlordsRequest : IRequest<LandlordsViewModel>
    {
        public string UserId { get; set; }
        public FilterDto Filters { get; set; }
    }
}
