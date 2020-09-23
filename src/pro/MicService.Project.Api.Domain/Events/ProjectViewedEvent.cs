using MediatR;
using MicService.Project.Api.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Project.Api.Domain.Events
{
    public class ProjectViewedEvent : INotification
    {
        public ProjectViewer Viewer { get; set; }
    }
}
