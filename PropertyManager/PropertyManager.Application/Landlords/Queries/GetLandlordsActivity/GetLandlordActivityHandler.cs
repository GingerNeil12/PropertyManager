using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Domain.Extensions;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity;

namespace PropertyManager.Application.Landlords.Queries.GetLandlordsActivity
{
    internal class GetLandlordActivityHandler
        : IRequestHandler<GetLandlordActivityRequest, LandlordActivityViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public GetLandlordActivityHandler(
            IApplicationDbContext context, 
            IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<LandlordActivityViewModel> Handle(
            GetLandlordActivityRequest request,
            CancellationToken cancellationToken)
        {
            var activities = from activity in _context.LandlordActivities
                             where activity.LandlordId == request.LandlordId
                             orderby activity.HappenedOn descending
                             select new LandlordActivityDto() 
                             {
                                 Id = activity.Id,
                                 Action = activity.Action,
                                 HappenedOn = activity.HappenedOn.ToStandardDateTimeString(),
                                 UserName = activity.UserId
                             };

            var sortColumn = request.Filter.SortColumn;
            var sortDirection = request.Filter.SortDirection;
            if(sortColumn != "Id")
            {
                if (!IsSortColumnAndDirectionEmpty(sortColumn, sortDirection))
                {
                    activities = activities.OrderBy(sortColumn + " " + sortDirection);
                }
            }

            var searchValue = request.Filter.SearchValue?.ToUpper();
            if(!string.IsNullOrWhiteSpace(searchValue))
            {
                activities = activities.Where(
                    x => x.Action.ToUpper().Contains(searchValue) ||
                    x.UserName.ToUpper().Contains(searchValue));
            }

            var totalRecords = activities.Count();
            activities = activities.Skip(request.Filter.Skip).Take(request.Filter.PageSize);
            var activitiesDto = activities.ToList();

            foreach (var activity in activitiesDto)
            {
                activity.UserName = await _identityService.GetUsersNameByIdAsync(activity.UserName);
            }

            var result = new LandlordActivityViewModel()
            {
                Activity = activitiesDto,
                TotalRecords = totalRecords
            };
            return result;
        }

        private bool IsSortColumnAndDirectionEmpty(string sortColumn, string sortDirection)
        {
            return IsSortColumnEmpty(sortColumn) && IsSortDirectionEmpty(sortDirection);
        }

        private bool IsSortColumnEmpty(string sortColumn)
        {
            return string.IsNullOrWhiteSpace(sortColumn);
        }

        private bool IsSortDirectionEmpty(string sortDirection)
        {
            return string.IsNullOrWhiteSpace(sortDirection);
        }
    }
}
