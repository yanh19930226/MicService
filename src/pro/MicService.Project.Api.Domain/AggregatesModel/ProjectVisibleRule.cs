using MicService.Project.Api.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Project.Api.Domain.AggregatesModel
{
    public class ProjectVisibleRule : Entity
    {
        public int ProjectId { get; set; }

        public bool Visible { get; set; }

        public string Tags { get; set; }
        public virtual Project Project { get; set; }
    }
}
