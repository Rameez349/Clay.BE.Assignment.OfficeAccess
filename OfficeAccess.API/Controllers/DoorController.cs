using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("Unlock/{doorId}")]
    [ProducesResponseType(typeof(AccessResponse), StatusCodes.Status200OK)]
    [TypeFilter(typeof(CustomAuthorization))]
    public async Task<ActionResult> UnlockDoor(long doorId)
    {
        long userId = HttpContextHelper.GetUserIdFromClaims();
        var accessResponse = HttpContextHelper.GetAccessResponseFromAuthorizationContext();

        await _doorsService.AddDoorAccessHistoryAsync(userId, doorId, accessResponse.AccessGranted);

        if (!accessResponse.AccessGranted)
            throw new UnauthorizedAccessException($"{ApiResponseMessages.Unauthorized} UserId:{userId}, DoorId:{doorId}");

        return Ok(accessResponse);
    }

    [HttpGet("AccessHistory/{doorId}")]
    [ProducesResponseType(typeof(IEnumerable<AccessHistoryResponse>), StatusCodes.Status200OK)]
    [Authorize(Policy = "HistoryAccess")]
    [TypeFilter(typeof(CustomAuthorization))]
    public async Task<ActionResult> AccessHistory(long doorId)
    {
        var accessResponse = HttpContextHelper.GetAccessResponseFromAuthorizationContext();

        if (accessResponse is null || !accessResponse.AccessGranted)
            throw new UnauthorizedAccessException($"{ApiResponseMessages.Unauthorized} DoorId:{doorId}");

        return Ok(await _doorsService.GetDoorAccessHistoryAsync(doorId));
    }
}
