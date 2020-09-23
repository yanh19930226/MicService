using Microsoft.EntityFrameworkCore;
using MicService.Project.Api.Domain.AggregatesModel;
using MicService.Project.Api.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicService.Project.Api.Infrastructure
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ProjectRepository(ProjectContext context)
        {
            _context = context;
        }
        public Domain.AggregatesModel.Project Add(Domain.AggregatesModel.Project project)
        {
            if (project.IsTransient())
            {
                return _context.Add(project).Entity;
            }
            else
            {
                return project;
            }
        }

        public async Task<Domain.AggregatesModel.Project> GetAsync(int id)
        {
            var project = await _context.Projects
                .Include(q => q.Viewers)
                .Include(q => q.Contributors)
                .Include(q => q.VisibleRule)
                .SingleOrDefaultAsync(q => q.Id == id);
            return project;
        }

        public Domain.AggregatesModel.Project Update(Domain.AggregatesModel.Project project)
        {
            return _context.Update(project).Entity;
        }
    }
}
