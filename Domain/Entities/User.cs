namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool AllowHistoryView { get; set; }
        public ICollection<UserAccessLevel> UserAccessLevels { get; set; } = null!;
    }
}
