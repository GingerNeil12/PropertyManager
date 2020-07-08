using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Domain.Enums;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;

namespace PropertyManager.Application.Landlords.Queries.GetLandlords
{
    public class GetLandlordsRequest : IRequest<LandlordsViewModel>
    {
        public string UserId { get; set; }
    }

    public class GetLandlordsRequestHandler
        : IRequestHandler<GetLandlordsRequest, LandlordsViewModel>
    {
        private IApplicationDbContext _context;
        private ILogger<GetLandlordsRequestHandler> _logger;

        public GetLandlordsRequestHandler(
            IApplicationDbContext context,
            ILogger<GetLandlordsRequestHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LandlordsViewModel> Handle(
            GetLandlordsRequest request,
            CancellationToken cancellationToken)
        {
            // All landlords that the user has 
            // That are not Archived or Deleted
            // TODO: Put PageCount/PageNumber into requets DTO
            // Take: PageCount
            // Skip: PageCount * PageNumber.
            var landlords = _context.Landlords
                .Where(x => x.CreatedBy.Equals(request.UserId)
                        && x.ActiveStatus == ActiveStatus.ACTIVE);



            var result = new LandlordsViewModel();
            return result;
        }
    }
}
