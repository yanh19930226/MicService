using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Project.Api.Applicatons.Queries
{
    public interface IProjectQueries
    {
        Task<dynamic> GetProjectDetail(int projectId);
        Task<dynamic> GetProjectsByUserId(int userId);
    }
}
