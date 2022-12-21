﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Door
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int OfficeId { get; set; }
        public Office Office { get; set; } = null!;
        public ICollection<DoorAccessLevel> DoorAccessLevels { get; set; } = null!;
    }
}
