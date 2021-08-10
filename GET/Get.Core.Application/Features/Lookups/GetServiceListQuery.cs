using GET.Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Features.Lookups
{
    public class GetServiceListQuery : IRequest<ReturnResult<List<ServiceVm>>>
    {
    }
}
