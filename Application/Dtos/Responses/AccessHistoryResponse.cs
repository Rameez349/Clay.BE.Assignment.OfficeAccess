namespace Application.Dtos.Responses
{
    public class AccessHistoryResponse
    {
        public long UserId { get; set; }
        public string Username { get; set; } = null!;
        public long DoorId { get; set; }
        public string Doorname { get; set; } = null!;
        public bool AccessGranted { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
