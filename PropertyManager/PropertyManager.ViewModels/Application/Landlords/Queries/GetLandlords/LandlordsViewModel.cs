using System.Collections.Generic;

namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class LandlordsViewModel
    {
        public FilterDto Filter { get; set; }
        public IEnumerable<LandlordsDto> Landlords { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
