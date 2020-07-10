﻿namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlords
{
    public class FilterDto
    {
        public int Skip { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string SearchValue { get; set; }
    }
}
