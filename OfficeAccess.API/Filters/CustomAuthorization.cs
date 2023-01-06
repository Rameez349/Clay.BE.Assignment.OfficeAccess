using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OfficeAccess.API.Filters
{
    public class CustomAuthorization : IAsyncAuthorizationFilter
    {
        private readonly IDoorsService _doorsService;

        public CustomAuthorization(IDoorsService doorsService)
        {
            _doorsService = doorsService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            int userId = int.Parse(context.HttpContext.Request.Query["UserId"]);
            int doorId = int.Parse(context.HttpContext.Request.Query["DoorId"]);

            var accessResponse = await _doorsService.AuthorizeViewDoorAccessHistoryAsync(userId, doorId);

            if (!accessResponse.AccessGranted)
                throw new UnauthorizedAccessException($"{ApiResponseMessages.Unauthorized} UserId:{userId}, DoorId:{doorId}");
        }
    }
}
