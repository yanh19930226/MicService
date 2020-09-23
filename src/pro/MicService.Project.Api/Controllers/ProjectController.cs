using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicService.Project.Api.Applicatons.Commands;
using MicService.Project.Api.Domain.AggregatesModel;

namespace MicService.Project.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    /// <returns></returns>
    [Route("api/project")]
    [ApiController]
    public class ProjectController : Controller
    {
        private IMediator _mediator;
        //private IRecommendService _recommendService;
        //private IProjectQueries _projectQueries;
        public ProjectController(IMediator mediator/*, IRecommendService recommendService, IProjectQueries projectQueries*/)
        {
            _mediator = mediator;
            //_recommendService = recommendService;
            //_projectQueries = projectQueries;
        }
        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProjec([FromBody]Domain.AggregatesModel.Project project)
        {
            if (project == null)
            {
                throw new ArgumentException("错误");
            }
            project.UserId = 1;
            var command = new CreateProjectCommand()
            {
                Project = project
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        /// <summary>
        /// 查看项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("view/{projectId}")]
        public async Task<IActionResult> ViewProjec(int projectId)
        {
            //if (await _recommendService.IsRecommendProject(projectId, UserIdentity.UserId))
            //{
            //    return BadRequest("无权限");
            //}
            var command = new ViewProjectCommand()
            {
                ProjectId = projectId,
                UserId = UserIdentity.UserId,
                UserName = UserIdentity.Name,
                Avatar = UserIdentity.Avatar
            };
            await _mediator.Send(command);
            return Ok();
        }
        /// <summary>
        /// 加入项目
        /// </summary>
        /// <param name="contributor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("join")]
        public async Task<IActionResult> JoinProject([FromBody]ProjectContributor contributor)
        {
            var command = new JoinProjectCommand()
            {
                Contributor = contributor
            };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
