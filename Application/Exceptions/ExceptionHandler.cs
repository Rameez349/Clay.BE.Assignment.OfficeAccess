using System.Net;
using Application.Dtos.Responses;
using Domain.Exceptions;
using Domain.Constants;
using Microsoft.Extensions.Logging;

namespace Application.Exceptions
{
    public static class ExceptionHandler
    {
        public static ErrorResponse HandleException(Exception ex, ILogger logger)
        {
            var response = new ErrorResponse(HttpStatusCode.InternalServerError, ex.Message, !string.IsNullOrEmpty(ex.InnerException?.Message) ? ex.InnerException.Message : ApiResponseMessages.Internal);

            switch (ex)
            {
                case NotFoundException:
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Content = ApiResponseMessages.Notfound;
                    break;
                case UnauthorizedAccessException:
                    response.StatusCode = HttpStatusCode.Forbidden;
                    response.Content = ApiResponseMessages.Unauthorized;
                    break;
                default:
                    logger.LogError(
                        "{exceptionType} occured with message {message}, {StackTrace}\n{Data}",
                        ex.GetType(),
                        ex.Message,
                        ex.StackTrace,
                        ex.Data);
                    break;
            }

            return response;
        }
    }
}
