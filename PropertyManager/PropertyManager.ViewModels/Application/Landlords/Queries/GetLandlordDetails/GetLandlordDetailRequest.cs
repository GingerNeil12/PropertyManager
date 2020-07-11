using MediatR;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails
{
    public class GetLandlordDetailRequest : IRequest<LandlordDetailViewModel>
    {
        public string LandlordId { get; set; }
    }
}
