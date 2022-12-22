using Application.Constants;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OfficeAccess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUserAccessService _userAccessService;
        private readonly IAccessHistoryService _accessHistoryService;

        public AccessController(IUserAccessService userAccessService, IAccessHistoryService accessHistoryService)
        {
            _userAccessService = userAccessService;
            _accessHistoryService = accessHistoryService;
        }

        [HttpGet("Door")]
        [ProducesResponseType(typeof(AccessResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> AccessDoor([FromQuery] AccessRequest accessRequest)
        {
            var accessResponse = await _userAccessService.AuthorizeUserAccessAsync(accessRequest.UserId, accessRequest.DoorId);

            await _accessHistoryService.AddAccessHistoryAsync(accessRequest.UserId, accessRequest.DoorId, accessResponse.AccessGranted);

            if (!accessResponse.AccessGranted)
                throw new UnauthorizedAccessException($"{ApiResponseMessages.Unauthorized} UserId:{accessRequest.UserId}, DoorId:{accessRequest.DoorId}");

            return Ok(accessResponse);
        }
    }
}
