using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GET.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
