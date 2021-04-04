using AutoMapper;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts.Infrastructure;
using GET.Core.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class GetServiceRequestExportExcelQueryHandler : IRequestHandler<GetServiceRequestExportExcelQuery, ReturnResult<ServiceRequestExportFileVm>>
    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestRepository _requestRepository;
        private readonly IExcelExporter _excelExporter;
        private readonly ILogger<GetServiceRequestExportExcelQueryHandler> _logger;

        public GetServiceRequestExportExcelQueryHandler(IMapper mapper, IServiceRequestRepository requestRepository, IExcelExporter excelExporter,
                                                        ILogger<GetServiceRequestExportExcelQueryHandler> logger)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
            _excelExporter = excelExporter;
            _logger = logger;
        }
        public async Task<ReturnResult<ServiceRequestExportFileVm>> Handle(GetServiceRequestExportExcelQuery request, CancellationToken cancellationToken)
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
                var fileData = _excelExporter.ExportServiceRequestsToExcel(allRequests);

                var requestsExportFileDto = new ServiceRequestExportFileVm { ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Data = fileData, ExportFileName = $"{Guid.NewGuid()}.xlsx" };

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
