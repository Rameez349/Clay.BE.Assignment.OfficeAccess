namespace Application.Dtos.Responses
{
    public class AccessResponse
    {
        public int UserId { get; init; }
        public int DoorId { get; init; }
        public bool AccessGranted { get; init; }
    }
}
