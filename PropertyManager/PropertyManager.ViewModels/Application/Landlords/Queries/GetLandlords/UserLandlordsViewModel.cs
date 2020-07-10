using System.Collections.Generic;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class UserLandlordsViewModel
    {
        public IEnumerable<LandlordsDto> Landlords { get; set; }
        public int TotalRecords { get; set; }
    }
}
