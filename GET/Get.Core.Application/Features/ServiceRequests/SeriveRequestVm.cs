using System;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class SeriveRequestVm
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public string UserName { get; set; }
        public string ServiceName { get; set; }
        public string StatusName { get; set; }
    }
}
