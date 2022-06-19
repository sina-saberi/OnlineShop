using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entites.Security
{
    public class Role
    {
        public Guid Id { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }
    }
}
