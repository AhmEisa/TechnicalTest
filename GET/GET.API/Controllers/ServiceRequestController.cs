using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GET.Core.Application.Features.ServiceRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet(Name = "GetAllServiceRequest")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SeriveRequestVm>>> GetAllRequests()
        {
            var response = await _mediator.Send(new GetAllUserServiceRequestQuery());
            return StatusCode((int)response.HttpCode, response);
        }


        [HttpGet("export-to-pdf", Name = "ExportServiceRequestPdf")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ExportServiceRequestPdf()
        {
            var response = await _mediator.Send(new GetServiceRequestExportQuery());

            if (response.IsSuccess)
                return File(response.Data.Data, response.Data.ContentType, response.Data.ExportFileName);

            return StatusCode((int)response.HttpCode, response);
        }


        [HttpGet("export-to-excel", Name = "ExportServiceRequestExcel")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ExportServiceRequestExcel()
        {
            var response = await _mediator.Send(new GetServiceRequestExportExcelQuery());

            if (response.IsSuccess)
                return File(response.Data.Data, response.Data.ContentType, response.Data.ExportFileName);

            return StatusCode((int)response.HttpCode, response);
        }

        [HttpGet("for-logged-in-user", Name = "GetAllServiceRequestForUser")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SeriveRequestVm>>> GetAllRequestsForUser()
        {
            var response = await _mediator.Send(new GetUserServiceRequestQuery());
            return StatusCode((int)response.HttpCode, response);
        }


        [HttpPost(Name = "AddServiceRequest")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SeriveRequestVm>>> Create([FromBody] CreateServiceRequestCommand createServiceRequestCommand)
        {
            var response = await _mediator.Send(createServiceRequestCommand);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPut("update-status", Name = "UpdateRequestStatus")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SeriveRequestVm>>> UpdateRequestStatus([FromBody] UpdateServiceRequestStatusCommand updateServiceRequestCommand)
        {
            var response = await _mediator.Send(updateServiceRequestCommand);
            return StatusCode((int)response.HttpCode, response);
        }


        [HttpPut(Name = "UpdateServiceRequest")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SeriveRequestVm>>> Update([FromBody] UpdateServiceRequestCommand updateServiceRequestCommand)
        {
            var response = await _mediator.Send(updateServiceRequestCommand);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpDelete("{id}", Name = "DeleteServiceRequest")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SeriveRequestVm>>> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteServiceRequestCommand { RequestId = id });
            return StatusCode((int)response.HttpCode, response);
        }
    }
}