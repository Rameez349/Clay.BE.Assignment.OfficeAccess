namespace Domain.Entities
{
    public class AccessHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int DoorId { get; set; }
        public Door Door { get; set; } = null!;
        public bool AccessGranted { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
