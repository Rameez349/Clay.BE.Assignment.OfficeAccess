using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserAccessLevel
    {
        public int AccessLevelId { get; set; }
        public AccessLevel AccessLevel { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
