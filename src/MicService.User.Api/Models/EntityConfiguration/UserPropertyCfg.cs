using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicService.User.Api.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Models.EntityConfiguration
{
    public class UserPropertyCfg : IEntityTypeConfiguration<Api.Models.Domain.UserProperty>
    {
        public void Configure(EntityTypeBuilder<UserProperty> builder)
        {
        }
    }
}
