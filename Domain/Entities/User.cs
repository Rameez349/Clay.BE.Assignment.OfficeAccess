namespace Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public bool AllowHistoryView { get; set; }
        public ICollection<UserAccessLevel> UserAccessLevels { get; set; } = null!;
    }
}
