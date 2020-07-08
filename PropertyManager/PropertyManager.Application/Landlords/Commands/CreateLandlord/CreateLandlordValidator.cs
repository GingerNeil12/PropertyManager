using System;
using System.Data;
using System.Linq;
using FluentValidation;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Domain.Enums;
using PropertyManager.Domain.Models.Landlords;
using PropertyManager.ViewModels.Application.Landlords.Commands;

namespace PropertyManager.Application.Landlords.Commands.CreateLandlord
{
    internal class CreateLandlordValidator : AbstractValidator<CreateLandlordRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreateLandlordValidator(IApplicationDbContext context)
        {
            _context = context;

            SetRules();
        }

        private void SetRules()
        {
            RuleFor(x => x.Title)
                            .MaximumLength(10);

            RuleFor(x => x.FirstName)
                .MaximumLength(50)
                .NotNull();

            RuleFor(x => x.MiddleNames)
                .MaximumLength(256);

            RuleFor(x => x.LastName)
                .MaximumLength(50)
                .NotNull();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotNull()
                .Custom((email, context) =>
                {
                    var result = _context.Landlords
                    .Where(x => x.Email.Equals(email))
                    .FirstOrDefault();

                    if (IsActiveOrArchivedLandlordRecord(result))
                    {

                        context.AddFailure("Email already in use.");
                    }
                });

            RuleFor(x => x.MobilePhone)
                .MaximumLength(20)
                .NotNull();

            RuleFor(x => x.HomePhone)
                .MaximumLength(20);

            RuleFor(x => x.Dob)
                .Must(BeBeforeCurrentDate).WithMessage("Dob cannot be in the future.")
                .NotNull();

            RuleFor(x => x.RegisterNumber)
                .MaximumLength(50)
                .NotNull()
                .Custom((number, context) =>
                {
                    var result = _context.Landlords
                    .Where(x => x.RegsiterNumber.Equals(number))
                    .FirstOrDefault();

                    if (IsActiveOrArchivedLandlordRecord(result))
                    {
                        context.AddFailure("Register Number already in use.");
                    }
                });
        }

        private bool IsActiveOrArchivedLandlordRecord(Landlord landlord)
        {
            return landlord != null && landlord.ActiveStatus != ActiveStatus.DELETED;
        }

        private bool BeBeforeCurrentDate(DateTime date)
        {
            return DateTime.Now > date;
        }
    }
}
