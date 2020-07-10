using PropertyManager.Domain.Enums;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class LandlordsDto
    {
        public string LandlordId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string ApprovalStatusString => ApprovalStatus.ToString();
        public string RegisterNumber { get; set; }
    }
}
