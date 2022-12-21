using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Clay.BE.Assignment.RoomAccess.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Clay.BE.Assignment.RoomAccess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessHistoryController : ControllerBase
    {
        private readonly IUserAccessService _userAccessService;
        private readonly IAccessHistoryService _accessHistoryService;

        public AccessHistoryController(IUserAccessService userAccessService, IAccessHistoryService accessHistoryService)
        {
            _userAccessService = userAccessService;
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
