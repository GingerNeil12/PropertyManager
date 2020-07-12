namespace PropertyManager.ViewModels.Application.Landlords.Queries.GetLandlordsActivity
{
    public class LandlordActivityDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string HappenedOn { get; set; }
    }
}
