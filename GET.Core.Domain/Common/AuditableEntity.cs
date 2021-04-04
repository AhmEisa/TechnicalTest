using System;

namespace GET.Core.Domain
{
    public class AuditableEntity
    {
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
