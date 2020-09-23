using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicService.Project.Api.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Project.Api.Infrastructure.EntityConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.Project>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.Project> builder)
        {
            builder.HasMany(p => p.Viewers)
                  .WithOne(p => p.Project)
                  .HasForeignKey(p => p.ProjectId);

            builder.HasMany(p => p.Contributors)
                 .WithOne(p => p.Project)
                 .HasForeignKey(p => p.ProjectId);

            builder.HasMany(p => p.Properties)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectId);

            builder.HasOne(p => p.VisibleRule)
                .WithOne(p => p.Project)
                .HasForeignKey<ProjectVisibleRule>(p => p.ProjectId);
        }
    }
}
