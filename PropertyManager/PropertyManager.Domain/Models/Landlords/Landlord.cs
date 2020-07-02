using System.Collections.Generic;
using PropertyManager.Domain.Enums;
using PropertyManager.Domain.Models.Base;

namespace PropertyManager.Domain.Models.Landlords
{
    public class Landlord : Person
    {
        public ActiveStatus ActiveStatus { get; set; }
        public string RegsiterNumber { get; set; }

        public virtual LandlordApprovalRecord LandlordApprovalRecord { get; set; }
        public virtual LandlordContactAddress ContactAddress { get; set; }

        public virtual IEnumerable<LandlordActivity> Activity { get; set; }
        public virtual IEnumerable<LandlordNote> Notes { get; set; }
    }
}
