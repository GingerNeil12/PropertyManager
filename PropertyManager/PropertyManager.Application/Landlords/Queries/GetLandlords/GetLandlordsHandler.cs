using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Domain.Enums;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords;

namespace PropertyManager.Application.Landlords.Queries.GetLandlords
{
    public class GetLandlordsHandler
        : IRequestHandler<GetLandlordsRequest, UserLandlordsViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetLandlordsHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserLandlordsViewModel> Handle(
            GetLandlordsRequest request,
            CancellationToken cancellationToken)
        {
            var landlordsDto = from landlord in _context.Landlords
                               join approval in _context.LandlordApprovalRecords
                               on landlord.Id equals approval.LandlordId
                               where landlord.CreatedBy == request.UserId
                               && landlord.ActiveStatus == ActiveStatus.ACTIVE
                               select new LandlordsDto()
                               {
                                   LandlordId = landlord.Id,
                                   Email = landlord.Email,
                                   FirstName = landlord.FirstName,
                                   LastName = landlord.LastName,
                                   MobilePhone = landlord.MobilePhone,
                                   HomePhone = landlord.HomePhone,
                                   RegisterNumber = landlord.RegsiterNumber,
                                   ApprovalStatus = approval.ApprovalStatus
                               };

            var sortColumn = request.Filters.SortColumn;
            var sortDirection = request.Filters.SortDirection;
            if (sortColumn == "ApprovalStatus")
            {
                landlordsDto = landlordsDto.OrderBy(x => x.ApprovalStatus);
                if (sortDirection != "asc")
                {
                    landlordsDto = landlordsDto.OrderByDescending(x => x.ApprovalStatus);
                }
            }
            else
            {
                if (!IsSortColumnAndDirectionEmpty(sortColumn, sortDirection))
                {
                    landlordsDto = landlordsDto.OrderBy(sortColumn + " " + sortDirection);
                }
            }

            var searchValue = request.Filters.SearchValue?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                landlordsDto = landlordsDto.Where(
                    x => x.LastName.ToUpper().Contains(searchValue));
            }

            var totalRecords = landlordsDto.Count();
            landlordsDto = landlordsDto.Skip(request.Filters.Skip).Take(request.Filters.PageSize);
            var result = new UserLandlordsViewModel()
            {
                Landlords = landlordsDto,
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
