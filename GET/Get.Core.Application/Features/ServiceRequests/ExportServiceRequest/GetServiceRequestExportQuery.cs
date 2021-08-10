using GET.Core.Application.Models;
using GET.Core.Domain;
using MediatR;
using System.Text;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class GetServiceRequestExportQuery : IRequest<ReturnResult<ServiceRequestExportFileVm>>
    {
    }
}
