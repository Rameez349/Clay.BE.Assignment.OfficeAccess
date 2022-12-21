using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Office
    {
        public Office()
        {
            Doors = new HashSet<Door>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Door> Doors { get; set; }
    }
}
