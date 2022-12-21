using System.Net;
using Application.Exceptions;

namespace Application.Dtos.Responses
{
    public class HttpErrorResponse
    {
        public HttpStatusCode HttpStatusCode { get; }

        public bool IsSuccess { get; }

        public string Message { get; }

        public string Content { get; }

        public HttpErrorResponse(
            HttpStatusCode statusCode,
            string message,
            string content)
        {
            HttpStatusCode = statusCode;
            IsSuccess = false;
            Message = message;
            Content = content;
        }
    }
}
