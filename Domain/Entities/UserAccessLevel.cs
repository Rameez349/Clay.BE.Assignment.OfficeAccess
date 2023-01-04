namespace Domain.Entities
{
    public class UserAccessLevel
    {
        public long AccessLevelId { get; set; }
        public AccessLevel AccessLevel { get; set; } = null!;
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
