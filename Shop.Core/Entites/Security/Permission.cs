using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entites.Security
{
    public class Permission
    {
        public Guid Id { get; set; }

        public string PermissionFlag { get; set; }

        public string Title { get; set; }

    }
}
