namespace Domain.Entities
{
    public class DoorAccessLevel
    {
        public long DoorId { get; set; }
        public Door Door { get; set; } = null!;
        public long AccessLevelId { get; set; }
        public AccessLevel AccessLevel { get; set; } = null!;
    }
}
