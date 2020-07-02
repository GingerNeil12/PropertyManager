using System;
using PropertyManager.Domain.Common;

namespace PropertyManager.Domain.Models.Base
{
    public abstract class Person : AuditableEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public DateTime Dob { get; set; }
    }
}
