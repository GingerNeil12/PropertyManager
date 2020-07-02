using PropertyManager.Domain.Common;

namespace PropertyManager.Domain.Models.Base
{
    public abstract class Address : AuditableEntity
    {
        public string Id { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string PostalArea { get; set; }
        public string PostalTown { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
    }
}
