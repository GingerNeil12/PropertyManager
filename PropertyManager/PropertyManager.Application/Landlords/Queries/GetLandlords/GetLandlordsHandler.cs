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
        : IRequestHandler<GetLandlordsRequest, LandlordsViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetLandlordsHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LandlordsViewModel> Handle(
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
                                   ApprovalStatus = approval.ApprovalStatus.ToString()
                               };

            if (!(string.IsNullOrWhiteSpace(request.Filters.SortColumn) && string.IsNullOrWhiteSpace(request.Filters.SortDirection)))
            {
                landlordsDto = landlordsDto.OrderBy(request.Filters.SortColumn + " " + request.Filters.SortDirection);
            }

            var searchValue = request.Filters.SearchValue;
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                landlordsDto = landlordsDto.Where(
                    x => x.LastName.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.Email.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.RegisterNumber.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.ApprovalStatus.ToUpper().Contains(searchValue.ToUpper()));
            }

            var totalRecords = landlordsDto.Count();
            landlordsDto = landlordsDto.Skip(request.Filters.Skip).Take(request.Filters.PageSize);
            var result = new LandlordsViewModel()
            {
                Landlords = landlordsDto,
                TotalRecords = totalRecords
            };
            return result;
        }
    }
}
