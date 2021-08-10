using GET.Core.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class GetAllUserServiceRequestQuery : IRequest<ReturnResult<List<SeriveRequestVm>>>
    {
    }
}
