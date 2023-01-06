namespace Application.Dtos.Responses
{
    public class AccessResponse
    {
        public long UserId { get; init; }
        public long DoorId { get; init; }
        public bool AccessGranted { get; init; }
    }
}
