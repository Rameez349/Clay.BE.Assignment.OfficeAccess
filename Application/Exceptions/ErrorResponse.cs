using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
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
}
