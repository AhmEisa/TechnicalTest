using GET.Core.Application.Features.ServiceRequests;
using System.Collections.Generic;

namespace GET.Core.Application.Contracts.Infrastructure
{
    public interface IExcelExporter
    {
        byte[] ExportServiceRequestsToExcel(List<ServiceRequestExportDto> requestExportDtos);
    }
}
