using Application.Dtos.Responses;
using Domain.Constants;

namespace OfficeAccess.API.Helpers
{
    public static class HttpContextHelper
    {
        private static HttpContext HttpContext => new HttpContextAccessor().HttpContext!;

        public static int GetUserIdFromClaims()
        {
            var userClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");

            if (userClaim is null)
                throw new UnauthorizedAccessException($"{ApiResponseMessages.Unauthorized}");

            return Convert.ToInt32(userClaim.Value);
        }

        public static AccessResponse GetAccessResponseFromAuthorizationContext()
        {
            var accessResponse = (AccessResponse?)HttpContext.Items["AccessResponse"];

            if (accessResponse is null)
                throw new NullReferenceException(nameof(accessResponse));

            return accessResponse;
        }
    }
}
