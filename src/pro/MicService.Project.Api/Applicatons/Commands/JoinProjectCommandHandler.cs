using MediatR;
using MicService.Project.Api.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicService.Project.Api.Applicatons.Commands
{
    public class JoinProjectCommandHandler : AsyncRequestHandler<JoinProjectCommand>
    {
        private IProjectRepository _projectRepository; 
        public JoinProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        protected override async Task Handle(JoinProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(request.Contributor.ProjectId);
            if (project == null)
            {
                throw new Exception("异常");
            }
            if (project.UserId == request.Contributor.UserId)
            {
                throw new Exception("不能加人自己的项目");
            }
            project.AddContributor(request.Contributor);
            await _projectRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
