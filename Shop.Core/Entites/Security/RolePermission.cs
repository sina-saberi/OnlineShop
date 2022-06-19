using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entites.Security
{
    public class RolePermission
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
