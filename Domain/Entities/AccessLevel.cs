using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccessLevel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<DoorAccessLevel> DoorAccessLevels { get; set; } = null!;
        public ICollection<UserAccessLevel> UserAccessLevels { get; set; } = null!;
    }
}
