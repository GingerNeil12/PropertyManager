using System;

namespace PropertyManager.Domain.Models.Base
{
    public abstract class Activity
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime HappenedOn { get; set; }
    }
}
