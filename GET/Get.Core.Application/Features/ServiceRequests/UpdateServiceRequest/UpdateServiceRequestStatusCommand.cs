using GET.Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class UpdateServiceRequestStatusCommand : IRequest<ReturnResult<bool>>
    {
        public Guid RequestId { get; set; }
        public int StatusId { get; set; }
    }
}
