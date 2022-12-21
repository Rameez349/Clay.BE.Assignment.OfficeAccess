using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Mappings
{
    public static class AccessHistoryResponseMapper
    {

        public static AccessHistoryResponse MapToAccessHistoryResponseDto(this AccessHistory acessHistory)
        {
            return new AccessHistoryResponse
            {
                AccessGranted = acessHistory.AccessGranted,
                DoorId = acessHistory.DoorId,
                Doorname = acessHistory.Door.Name,
                UserId = acessHistory.UserId,
                Username = acessHistory.User.Name,
                Timestamp = acessHistory.Timestamp
            };
        }
    }
}
