using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Permissions
{
    public class Permission : Entity<PermissionId>
    {

        public Permission(PermissionId id, Nombre nombre) : base(id)
        {
            Nombre = nombre;
        }

        public Nombre? Nombre { get; init; }

        public Permission(Nombre nombre) : base()
        {

            Nombre = nombre;
        }

        public static Result<Permission> Create(Nombre nombre)
        {
            return new Permission(nombre);
        }
    }
}
