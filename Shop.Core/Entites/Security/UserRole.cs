using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entites.Security
{
    public class UserRole
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
