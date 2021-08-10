using AutoMapper;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts.Infrastructure;
using GET.Core.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class GetServiceRequestExportQueryHandler : IRequestHandler<GetServiceRequestExportQuery, ReturnResult<ServiceRequestExportFileVm>>
    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestRepository _requestRepository;
        private readonly IPdfExporter _pdfExporter;
        private readonly ILogger<GetServiceRequestExportQueryHandler> _logger;

        public GetServiceRequestExportQueryHandler(IMapper mapper, IServiceRequestRepository requestRepository,
                                                  IPdfExporter pdfExporter,ILogger<GetServiceRequestExportQueryHandler> logger)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
            _pdfExporter = pdfExporter;
            _logger = logger;
        }
        public async Task<ReturnResult<ServiceRequestExportFileVm>> Handle(GetServiceRequestExportQuery request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<ServiceRequestExportFileVm>();
            try
            {
                var allRequests = _mapper.Map<List<ServiceRequestExportDto>>(await _requestRepository.GetAllUserRequestsAsync());
                if (allRequests == null || allRequests.Count == 0)
                {
                    response.DefaultNotFound();
                    return response;
                }
                var fileData = _pdfExporter.ExportServiceRequestsToPdf(allRequests);

                var requestsExportFileDto = new ServiceRequestExportFileVm { ContentType = "application/pdf", Data = fileData, ExportFileName = $"{Guid.NewGuid()}.pdf" };

                response.Success(requestsExportFileDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                response.DefaultServerError();
            }

            return response;
        }
    }
}
