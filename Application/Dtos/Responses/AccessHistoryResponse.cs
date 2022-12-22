namespace Application.Dtos.Responses
{
    public class AccessHistoryResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public int DoorId { get; set; }
        public string Doorname { get; set; } = null!;
        public bool AccessGranted { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
