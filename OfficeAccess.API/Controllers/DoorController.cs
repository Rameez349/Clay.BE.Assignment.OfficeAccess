using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using OfficeAccess.API.Filters;
using OfficeAccess.API.Helpers;

namespace OfficeAccess.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoorController : ControllerBase
{
    private readonly IDoorsService _doorsService;

    public DoorController(IDoorsService doorsService)
    {
        _doorsService = doorsService;
    }

    [HttpPost("Unlock/{DoorId}")]
    [ProducesResponseType(typeof(AccessResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> UnlockDoor(int DoorId)
    {
        int userId = HttpContextHelper.GetUserIdFromClaims();

        var accessResponse = await _doorsService.AuthorizeDoorAccessAsync(userId, DoorId);

        await _doorsService.AddDoorAccessHistoryAsync(userId, DoorId, accessResponse.AccessGranted);

        if (!accessResponse.AccessGranted)
            throw new UnauthorizedAccessException($"{ApiResponseMessages.Unauthorized} UserId:{userId}, DoorId:{DoorId}");

        return Ok(accessResponse);
    }

    [HttpGet("Door")]
    [ProducesResponseType(typeof(IEnumerable<AccessHistoryResponse>), StatusCodes.Status200OK)]
    [TypeFilter(typeof(CustomAuthorization))]
    public async Task<ActionResult> AccessHistory([FromQuery] AccessRequest accessRequest)
    {
        return Ok(await _doorsService.GetDoorAccessHistoryAsync(accessRequest.DoorId));
    }
}
