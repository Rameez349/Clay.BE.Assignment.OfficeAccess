namespace Domain.Entities
{
    public class AccessLevel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<DoorAccessLevel> DoorAccessLevels { get; set; } = null!;
        public ICollection<UserAccessLevel> UserAccessLevels { get; set; } = null!;
    }
}
