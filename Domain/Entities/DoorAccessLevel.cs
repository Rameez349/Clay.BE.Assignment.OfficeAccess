using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DoorAccessLevel
    {
        public int DoorId { get; set; }
        public Door Door { get; set; } = null!;
        public int AccessLevelId { get; set; }
        public AccessLevel AccessLevel { get; set; } = null!;
    }
}
