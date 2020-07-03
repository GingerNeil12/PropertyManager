using FluentValidation;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.ViewModels.Application.Landlords.Commands;

namespace PropertyManager.Application.Landlords.Commands.CreateLandlord
{
    internal class CreateLandlordValidator : AbstractValidator<CreateLandlordRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreateLandlordValidator(IApplicationDbContext context)
        {
            _context = context;
        }
    }
}
