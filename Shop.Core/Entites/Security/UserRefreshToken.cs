using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entites.Security
{
    public class UserRefreshToken
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public string Refreshtoken { get; set; }

        public int RefreshTokenTieOut { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsValid { get; set; }
    }
}
