using System.Net;

namespace Application.Dtos.Responses;

public class ErrorResponse
{
    public ErrorResponse(HttpStatusCode statusCode, string message, string content)
    {
        StatusCode = statusCode;
        Message = message;
        Content = content;
    }

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public string Content { get; set; }
}
