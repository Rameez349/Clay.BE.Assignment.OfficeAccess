namespace Domain.Entities
{
    public class Office
    {
        public Office()
        {
            Doors = new HashSet<Door>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Door> Doors { get; set; }
    }
}
