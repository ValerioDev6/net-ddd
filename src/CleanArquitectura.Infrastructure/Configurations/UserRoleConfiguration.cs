using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArquitectura.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("users_roles");

            builder.HasKey(x => new { x.RoleId, x.UserId });
            builder.Property(user => user.UserId);

            builder.Property(user => user.UserId)
                .HasConversion(userID => userID!.Value, value => new UserId(value));
        }
    }
}
