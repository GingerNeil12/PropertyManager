using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Domain.Enums;
using PropertyManager.Domain.Models.Landlords;

namespace PropertyManager.Application.Landlords.Commands.CreateLandlord
{
    internal class LandlordCreatedNotification : INotification
    {
        public string LandlordId { get; private set; }

        public LandlordCreatedNotification(string landlordId)
        {
            LandlordId = landlordId;
        }
    }

    internal class CreateLandlordApprovalRecord
        : INotificationHandler<LandlordCreatedNotification>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ILogger<CreateLandlordApprovalRecord> _logger;

        public CreateLandlordApprovalRecord(
            IApplicationDbContext context,
            IDateTime dateTime,
            ILogger<CreateLandlordApprovalRecord> logger)
        {
            _context = context;
            _dateTime = dateTime;
            _logger = logger;
        }

        public Task Handle(
            LandlordCreatedNotification notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating Approval Record for: {notification.LandlordId}");
            var approvalRecord = new LandlordApprovalRecord()
            {
                Id = Guid.NewGuid().ToString(),
                ApprovalStatus = ApprovalStatus.SUBMITTED,
                SubmittedOn = _dateTime.Now,
                LandlordId = notification.LandlordId
            };

            _context.LandlordApprovalRecords.Add(approvalRecord);

            return Task.CompletedTask;
        }
    }

    internal class CreateLandlordCreatedActivity
        : INotificationHandler<LandlordCreatedNotification>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CreateLandlordCreatedActivity> _logger;

        public CreateLandlordCreatedActivity(
            IApplicationDbContext context, 
            IDateTime dateTime, 
            ICurrentUser currentUser, 
            ILogger<CreateLandlordCreatedActivity> logger)
        {
            _context = context;
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(
            LandlordCreatedNotification notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"Creating Activity for new lanlord: {notification.LandlordId}");

            var activity = new LandlordActivity()
            {
                Id = Guid.NewGuid().ToString(),
                Action = "Landlord Created.",
                Description = "Landlord Created.",
                UserId = _currentUser.UserId,
                HappenedOn = _dateTime.Now,
                LandlordId = notification.LandlordId
            };

            _context.LandlordActivities.Add(activity);

            return Task.CompletedTask;
        }
    }
}
