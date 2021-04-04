using System;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class ServiceRequestExportDto
    {
        public Guid RequestId { get; set; }
        public string UserName { get; set; }
        public string ServiceName { get; set; }
        public string RequestStatus { get; set; }
    }
}
