using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Permissions;
using CleanArquitectura.Domain.Shared;

namespace CleanArquitectura.Domain.Roles
{
    public sealed class Role : Enumeration<Role>
    {
        public static readonly Role Cliente = new(1, "Cliente");
        public static readonly Role Admin = new(1, "Admin");

        public Role(int id, string name) : base(id, name)
        {

        }

        public ICollection<Permission>? Permissions { get; set; }
    }
}
