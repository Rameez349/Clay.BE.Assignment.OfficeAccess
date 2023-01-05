namespace Domain.Entities
{
    public class AccessHistory
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public long DoorId { get; set; }
        public Door Door { get; set; } = null!;
        public bool AccessGranted { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
