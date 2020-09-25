using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicService.Recommend.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Recommend.Api.EntityConfiguration
{
    public class ProjectRecommendConfiguration : IEntityTypeConfiguration<ProjectRecommend>
    {
        public void Configure(EntityTypeBuilder<ProjectRecommend> builder)
        {

        }
    }
}
