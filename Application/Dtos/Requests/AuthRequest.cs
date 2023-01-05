using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dtos.Requests
{
    public record AuthRequest
    {
        public long UserId { get; init; }
    };
}
