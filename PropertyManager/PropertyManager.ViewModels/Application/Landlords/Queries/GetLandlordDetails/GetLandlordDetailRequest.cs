using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails
{
    public class GetLandlordDetailRequest : IRequest<LandlordDetailViewModel>
    {
        [Required]
        public string LandlordId { get; set; }
    }
}
