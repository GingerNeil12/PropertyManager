using System.Collections.Generic;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity
{
    public class LandlordActivityViewModel
    {
        public IEnumerable<LandlordActivityDto> Activity { get; set; }
        public int TotalRecords { get; set; }
    }
}
