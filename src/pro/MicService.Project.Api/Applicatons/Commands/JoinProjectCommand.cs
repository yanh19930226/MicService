using MediatR;
using MicService.Project.Api.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Project.Api.Applicatons.Commands
{
    public class JoinProjectCommand : IRequest
    { 
        public ProjectContributor Contributor { get; set; }
    }
}
