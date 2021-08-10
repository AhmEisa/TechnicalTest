using Inetum.Domain.Common;
using System;

namespace Inetum.Domain.Entities
{
    public class Player : AuditableEntity
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }
}
