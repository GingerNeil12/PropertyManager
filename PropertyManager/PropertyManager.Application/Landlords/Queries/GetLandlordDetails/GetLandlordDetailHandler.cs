using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PropertyManager.Application.Common.Helpers;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Application.Exceptions;
using PropertyManager.Domain.Extensions;
using PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordDetails;

namespace PropertyManager.Application.Landlords.Queries.GetLandlordDetails
{
    internal class GetLandlordDetailHandler
        : IRequestHandler<GetLandlordDetailRequest, LandlordDetailViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public GetLandlordDetailHandler(
            IApplicationDbContext context,
            IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<LandlordDetailViewModel> Handle(
            GetLandlordDetailRequest request,
            CancellationToken cancellationToken)
        {
            var result = (from landlord in _context.Landlords
                          join approval in _context.LandlordApprovalRecords
                          on landlord.Id equals approval.LandlordId
                          where landlord.Id == request.LandlordId
                          select new LandlordDetailViewModel()
                          {
                              LandlordId = landlord.Id,
                              Email = landlord.Email,
                              HomePhone = landlord.HomePhone,
                              MobilePhone = landlord.MobilePhone,
                              Dob = landlord.Dob.ToStandardDateString(),
                              RegisterNumber = landlord.RegsiterNumber,
                              ActiveStatus = landlord.ActiveStatus.ToString(),
                              ApprovalStatus = approval.ApprovalStatus.ToString(),
                              FullName = NameHelper.FormatFullName(
                                  landlord.FirstName,
                                  landlord.LastName,
                                  landlord.Title,
                                  landlord.MiddleNames)
                          }).FirstOrDefault();

            if (result == null)
            {
                throw new NotFoundException("Unable to find Landlord.");
            }

            var approvalRecord = _context.LandlordApprovalRecords
                .Where(x => x.LandlordId.Equals(request.LandlordId))
                .FirstOrDefault();

            switch (result.ApprovalStatus)
            {
                case "APPROVED":
                    result.ApprovalStatusBy = await _identityService
                        .GetUsersNameByIdAsync(approvalRecord.ApprovedBy);
                    result.ApprovalStatusOn = approvalRecord.ApprovedOn.Value.ToStandardDateString();
                    break;
                case "SUBMITTED":
                    result.ApprovalStatusBy = await _identityService
                        .GetUsersNameByIdAsync(approvalRecord.CreatedBy);
                    result.ApprovalStatusOn = approvalRecord.SubmittedOn.ToStandardDateString();
                    break;
                case "REJECTED":
                    result.ApprovalStatusBy = await _identityService
                        .GetUsersNameByIdAsync(approvalRecord.RejectedBy);
                    result.ApprovalStatusOn = approvalRecord.RejectedOn.Value.ToStandardDateString();
                    break;
            }

            return result;
        }
    }
}
