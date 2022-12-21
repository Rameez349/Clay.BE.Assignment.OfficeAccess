using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Responses
{
    public class AccessResponse
    {
        public int UserId { get; init; }
        public int DoorId { get; init; }
        public bool AccessGranted { get; init; }
    }
}
