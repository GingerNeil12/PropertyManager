using System.Collections.Generic;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class LandlordsViewModel
    {
        public FilterDto Filter { get; set; }
        public IEnumerable<LandlordsDto> Landlords { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalResult { get; set; }
    }
}
