using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entites.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.FluentAPIConfiguration.Security
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.
                ToTable("Permissions")
                .HasKey(x => x.Id);
            builder.Property(x => x.PermissionFlag)
                .HasMaxLength(200);
            builder.Property(x => x.Title)
                .HasMaxLength(200);
        }
    }
}
