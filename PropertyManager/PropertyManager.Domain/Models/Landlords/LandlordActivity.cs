using PropertyManager.Domain.Models.Base;

namespace PropertyManager.Domain.Models.Landlords
{
    public class LandlordActivity : Activity
    {
        public string LandlordId { get; set; }

        public virtual Landlord Landlord { get; set; }
    }
}
