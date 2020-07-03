using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PropertyManager.ViewModels.Application.Landlords.Commands
{
    public class CreateLandlordRequest : IRequest<string>
    {
        [StringLength(maximumLength: 10)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 50)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name(s)")]
        [StringLength(maximumLength: 256)]
        public string MiddleNames { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        [StringLength(maximumLength: 20)]
        public string MobilePhone { get; set; }

        [Display(Name = "Home Phone")]
        [StringLength(maximumLength: 20)]
        public string HomePhone { get; set; }

        [Required]
        [Display(Name = "Register Number")]
        [StringLength(maximumLength: 50)]
        public string RegisterNumber { get; set; }

        [Required]
        public DateTime Dob { get; set; }
    }
}
