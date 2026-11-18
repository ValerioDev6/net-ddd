using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArquitectura.Domain.Users
{
    public sealed class UserRole
    {
        public int RoleId { get; set; }
        public UserId? UserId { get; set; }
    }
}
