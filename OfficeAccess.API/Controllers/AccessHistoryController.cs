using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using OfficeAccess.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace OfficeAccess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessHistoryController : ControllerBase
    {
        private readonly IAccessHistoryService _accessHistoryService;

        public AccessHistoryController(IAccessHistoryService accessHistoryService)
        {
            _accessHistoryService = accessHistoryService;
        }

        [HttpGet("Door")]
        [ProducesResponseType(typeof(IEnumerable<AccessHistoryResponse>), StatusCodes.Status200OK)]
        [TypeFilter(typeof(CustomAuthorization))]
        public async Task<ActionResult> AccessHistory([FromQuery] AccessRequest accessRequest)
        {
            return Ok(await _accessHistoryService.GetAccessHistoryAsync(accessRequest.DoorId));
        }
    }
}
