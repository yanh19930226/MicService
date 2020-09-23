using MediatR;
using MicService.Project.Api.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Project.Api.Domain.Events
{
    public class ProjectJoinedEvent : INotification
    {
        public ProjectContributor Contributor { get; set; }
    }
}
