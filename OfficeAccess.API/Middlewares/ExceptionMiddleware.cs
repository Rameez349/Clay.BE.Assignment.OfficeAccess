using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Dtos.Responses;
using Application.Exceptions;

namespace OfficeAccess.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = ExceptionHandler.HandleException(ex, _logger);

                context.Response.StatusCode = (int)response.StatusCode;
                context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
                var serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                };
                var resultJson = JsonSerializer.Serialize(new HttpErrorResponse(
                    response.StatusCode,
                    response.Message,
                    response.Content), serializerOptions);

                await context.Response.WriteAsync(resultJson);
            }
        }
    }
}
