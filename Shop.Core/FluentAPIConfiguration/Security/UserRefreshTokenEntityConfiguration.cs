using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entites;
using Shop.Core.Entites.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.FluentAPIConfiguration.Security
{
    internal class UserRefreshTokenEntityConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("RefresTokens");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Refreshtoken)
            .HasMaxLength(128);
        }
    }
}
