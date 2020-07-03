using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Application.Exceptions;
using PropertyManager.Domain.Enums;
using PropertyManager.Domain.Models.Landlords;
using PropertyManager.ViewModels.Application.Landlords.Commands;

namespace PropertyManager.Application.Landlords.Commands.CreateLandlord
{
    internal class CreateLandlordHandler 
        : IRequestHandler<CreateLandlordRequest, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly CreateLandlordValidator _validator;
        private readonly ILogger<CreateLandlordHandler> _logger;

        public CreateLandlordHandler(
            IApplicationDbContext context,
            IMediator mediator,
            ILogger<CreateLandlordHandler> logger)
        {
            _context = context;
            _mediator = mediator;
            _logger = logger;
            _validator = new CreateLandlordValidator(context);
        }

        public async Task<string> Handle(
            CreateLandlordRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating Landlord: {request.Email}");

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning($"Invalid Landlord: {request.Email}");
                throw new ValidationException(validationResult.Errors);
            }

            var landlord = new Landlord()
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                FirstName = request.FirstName,
                MiddleNames = request.MiddleNames,
                LastName = request.LastName,
                Email = request.Email,
                MobilePhone = request.MobilePhone,
                HomePhone = request.HomePhone,
                RegsiterNumber = request.RegisterNumber,
                Dob = request.Dob,
                ActiveStatus = ActiveStatus.ACTIVE
            };

            _context.Landlords.Add(landlord);
            await PublishLandlordCreatedNotification(landlord.Id);

            _logger.LogWarning($"Landlord Created: {landlord.Email}");
            return landlord.Id;
        }

        private async Task PublishLandlordCreatedNotification(string landlordId)
        {
            var notification = new LandlordCreatedNotification(landlordId);
            await _mediator.Publish(notification);
        }
    }

    internal class CreateLandlordPostRequestHandler
        : IRequestPostProcessor<CreateLandlordRequest, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateLandlordPostRequestHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Process(
            CreateLandlordRequest request,
            string response,
            CancellationToken cancellationToken)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DbContextException(
                    $"Unable to create landlord: {request.Email}", ex);
            }
        }
    }
}
