using GET.Core.Application.Models;
using MediatR;
using System;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class UpdateServiceRequestCommand : IRequest<ReturnResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public int StatusId { get; set; }
    }
}
