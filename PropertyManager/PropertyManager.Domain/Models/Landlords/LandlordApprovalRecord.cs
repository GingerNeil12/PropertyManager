using System;
using PropertyManager.Domain.Common;
using PropertyManager.Domain.Enums;

namespace PropertyManager.Domain.Models.Landlords
{
    public class LandlordApprovalRecord : AuditableEntity
    {
        public string Id { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime SubmittedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? RejectedOn { get; set; }
        public string RejectedBy { get; set; }
        public string LandlordId { get; set; }

        public virtual Landlord Landlord { get; set; }
    }
}
