using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Permissions;

namespace CleanArquitectura.Domain.Roles
{
    public sealed class RolePermission
    {
        public int RoleId { get; set; }
        public PermissionId? PermissionId { get; set; }
    }
}
