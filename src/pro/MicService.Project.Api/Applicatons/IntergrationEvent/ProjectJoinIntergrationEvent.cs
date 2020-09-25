using MicService.Project.Api.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Project.Api.Applicatons.IntergrationEvent
{
    public class ProjectJoinIntergrationEvent
    {
        public ProjectContributor Contributor { get; set; }
    }
}
