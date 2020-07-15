using System.ComponentModel.DataAnnotations;
using MediatR;
using PropertyManager.Domain.Enums;
using PropertyManager.ViewModels.Application.Common;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class GetLandlordsRequest : IRequest<UserLandlordsViewModel>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApprovalStatus ApprovalStatus { get; set; }

        [Required]
        public FilterDto Filters { get; set; }
    }
}
