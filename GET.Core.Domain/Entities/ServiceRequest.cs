using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GET.Core.Domain
{
    public class ServiceRequest : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public ServiceStatus ServiceStatus { get; set; }
        public bool IsDeleted { get; set; }
    }
}
