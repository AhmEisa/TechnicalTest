using GET.Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class CreateServiceRequestCommand : IRequest<ReturnResult<Guid>>
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public int StatusId { get; set; }
    }
}
