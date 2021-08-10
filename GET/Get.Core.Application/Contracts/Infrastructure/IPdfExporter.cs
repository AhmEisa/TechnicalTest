using GET.Core.Application.Features.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Contracts.Infrastructure
{
    public interface IPdfExporter
    {
        byte[] ExportServiceRequestsToPdf(List<ServiceRequestExportDto> requestExportDtos);
    }
}
