using MicService.Project.Api.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicService.Project.Api.Domain.AggregatesModel
{
    public interface IProjectRepository : IRepository<Project>
    {

        Task<Project> GetAsync(int id);

        Project Add(Project project);

        Project Update(Project project);
    }
}
