using System.ComponentModel.DataAnnotations;
using MediatR;
using PropertyManager.ViewModels.Application.Common;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity
{
    public class GetLandlordActivityRequest : IRequest<LandlordActivityViewModel>
    {
        [Required]
        public string LandlordId { get; set; }

        [Required]
        public FilterDto Filter { get; set; }
    }
}
