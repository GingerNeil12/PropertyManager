using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PropertyManager.Application.Common.Helpers;
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
                               orderby landlord.LastName ascending
                               select new LandlordsDto()
                               {
                                   Id = landlord.Id,
                                   MobilePhone = landlord.MobilePhone,
                                   HomePhone = landlord.HomePhone,
                                   Email = landlord.Email,
                                   ApprovalStatus = approval.ApprovalStatus.ToString(),
                                   RegisterNumber = landlord.RegsiterNumber,
                                   FullName = NameHelper.FormatFullName(
                                       landlord.FirstName,
                                       landlord.LastName,
                                       landlord.Title,
                                       landlord.MiddleNames)
                               };

            var totalRecords = landlordsDto.Count();

            landlordsDto = landlordsDto.Skip(0).Take(request.PageSize);

            var filterDto = GetFilterDto();

            var result = new LandlordsViewModel()
            {
                Filter = filterDto,
                Landlords = landlordsDto,
                PageNumber = 1,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (totalRecords + request.PageSize - 1) / request.PageSize
            };

            return result;
        }

        private FilterDto GetFilterDto()
        {
            var approvalStatusDto = new ApprovalStatusDto()
            {
                Value = (int)ApprovalStatus.APPROVED,
                Name = ApprovalStatus.APPROVED.ToString()
            };

            var approvalStatuses = Enum.GetValues(typeof(ApprovalStatus))
                .Cast<ApprovalStatus>()
                .Select(x => new ApprovalStatusDto()
                {
                    Value = (int)x,
                    Name = x.ToString()
                });

            var result = new FilterDto()
            {
                Name = string.Empty,
                Email = string.Empty,
                RegisterNumber = string.Empty,
                ApprovalStatus = approvalStatusDto,
                ApprovalStatuses = approvalStatuses
            };

            return result;
        }
    }
}
