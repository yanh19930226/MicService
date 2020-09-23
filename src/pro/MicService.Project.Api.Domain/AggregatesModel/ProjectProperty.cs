using MicService.Project.Api.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Project.Api.Domain.AggregatesModel
{
    public class ProjectProperty : Entity
    {
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }

        public ProjectProperty()
        {

        }
    }
}
