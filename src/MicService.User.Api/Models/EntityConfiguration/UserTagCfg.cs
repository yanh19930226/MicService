using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicService.User.Api.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Models.EntityConfiguration
{
    public class UserTagCfg : IEntityTypeConfiguration<Api.Models.Domain.UserTag>
    {
        public void Configure(EntityTypeBuilder<UserTag> builder)
        {
        }
    }
}
