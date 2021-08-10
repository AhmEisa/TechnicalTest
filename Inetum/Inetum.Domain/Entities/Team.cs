using Inetum.Domain.Common;
using System;
using System.Collections.Generic;

namespace Inetum.Domain.Entities
{
    public class Team : AuditableEntity
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime FoundationDate { get; set; }
        public string CoachName { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
