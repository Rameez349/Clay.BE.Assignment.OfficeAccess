using Application.Dtos.Responses;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficeAccess.API.Helpers;

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
            long userId = HttpContextHelper.GetUserIdFromClaims();
            long doorId = Convert.ToInt64(context.HttpContext.Request.RouteValues["doorId"]);

            context.HttpContext.Items["AccessResponse"] = await _doorsService.AuthorizeDoorAccessAsync(userId, doorId);
        }
    }
}
