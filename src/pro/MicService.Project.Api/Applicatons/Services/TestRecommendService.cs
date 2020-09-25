using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Project.Api.Applicatons.Services
{
    public class TestRecommendService : IRecommendService
    {
        public Task<bool> IsRecommendProject(int projectId, int userId)
        {
            return Task.FromResult(true);
        }
    }
}
