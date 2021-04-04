using GET.Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class GetUserServiceRequestQuery : IRequest<ReturnResult<List<SeriveRequestVm>>>
    {
        public Guid UserId { get; set; }
    }
}
