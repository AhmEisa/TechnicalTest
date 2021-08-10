using GET.Core.Application.Models;
using MediatR;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class GetServiceRequestExportExcelQuery : IRequest<ReturnResult<ServiceRequestExportFileVm>>
    {
    }
}
