using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Dtos.Requests
{
    public record AccessRequest
    {
        [FromQuery(Name = "DoorId"), BindRequired]
        public int DoorId { get; init; }
    }
}
