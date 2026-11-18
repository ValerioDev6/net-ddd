using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace CleanArquitectura.Infrastructure.Authentication
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionEnum permission) : base(policy: permission.ToString())
        {

        }
    }
}
