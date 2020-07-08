using System.Collections.Generic;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class FilterDto
    {
        public IEnumerable<ApprovalStatusDto> ApprovalStatuses { get; set; }
        public ApprovalStatusDto ApprovalStatus { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegisterNumber { get; set; }
    }
}
