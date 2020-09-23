using MediatR;
using Microsoft.EntityFrameworkCore;
using MicService.Project.Api.Domain.SeedWork;
using MicService.Project.Api.Infrastructure.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicService.Project.Api.Infrastructure
{
    public class ProjectContext : DbContext, IUnitOfWork
    {
        private IMediator _mediator;
        public ProjectContext(DbContextOptions<ProjectContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }
        public DbSet<Domain.AggregatesModel.Project> Projects { get; set; }
        public DbSet<Domain.AggregatesModel.ProjectViewer> ProjectViewers { get; set; }
        public DbSet<Domain.AggregatesModel.ProjectContributor> ProjectContributors { get; set; }
        public DbSet<Domain.AggregatesModel.ProjectVisibleRule> ProjectVisibleRules { get; set; }
        public DbSet<Domain.AggregatesModel.ProjectProperty> ProjectProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectViewerConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectContributorConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectVisibleRuleConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectPropertyConfiguration());

        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync();
            return true;
        }
    }
}
