namespace Domain.Entities
{
    public class Door
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long OfficeId { get; set; }
        public Office Office { get; set; } = null!;
        public ICollection<DoorAccessLevel> DoorAccessLevels { get; set; } = null!;
    }
}
