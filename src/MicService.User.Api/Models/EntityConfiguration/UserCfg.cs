using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Models.EntityConfiguration
{
    public class UserCfg : IEntityTypeConfiguration<Api.Models.Domain.User>
    {
        public void Configure(EntityTypeBuilder<Domain.User> builder)
        {
            builder.HasMany(p => p.Properties)
                  .WithOne(p => p.User)
                  .HasForeignKey(p => p.UserId);

            builder.HasMany(p => p.UserTags)
                  .WithOne(p => p.User)
                  .HasForeignKey(p => p.UserId);

            //builder.HasOne(p => p.BPFile)
            //    .WithOne(p => p.User)
            //    .HasForeignKey()
        }
    }
}
