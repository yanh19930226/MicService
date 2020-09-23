using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicService.Project.Api.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Project.Api.Infrastructure.EntityConfiguration
{
    public class ProjectPropertyConfiguration : IEntityTypeConfiguration<ProjectProperty>
    {
        public void Configure(EntityTypeBuilder<ProjectProperty> builder)
        {
        }
    }
}
