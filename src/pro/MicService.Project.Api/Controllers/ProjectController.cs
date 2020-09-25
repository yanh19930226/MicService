using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicService.Abstractions.IntegrationEvents;
using MicService.Project.Api.Applicatons.Commands;
using MicService.Project.Api.Applicatons.Queries;
using MicService.Project.Api.Applicatons.Services;
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
        private IRecommendService _recommendService;
        private IProjectQueries _projectQueries;
        private readonly ICapPublisher _capBus;
        public ProjectController(IMediator mediator, IRecommendService recommendService, IProjectQueries projectQueries, ICapPublisher capBus)
        {
            _mediator = mediator;
            _recommendService = recommendService;
            _projectQueries = projectQueries;
            _capBus = capBus;
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
            if (await _recommendService.IsRecommendProject(projectId, 1))
            {
                return BadRequest("无权限");
            }
            var command = new ViewProjectCommand()
            {
                ProjectId = projectId,
                UserId = 1,
                UserName = "test",
                Avatar = "test"
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
        /// <summary>
        /// 我的项目列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _projectQueries.GetProjectsByUserId(1));
        }
        /// <summary>
        /// 我的项目详细
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("my/{projectId}")]
        public async Task<IActionResult> GetProjectDetail(int projectId)
        {
            var project = await _projectQueries.GetProjectDetail(projectId);
            if (project.UserId == 1)
            {
                return Ok(project);
            }
            else
            {
                return BadRequest("无权限");
            }
        }
        /// <summary>
        /// 推荐项目详细
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recommend/{projectId}")]
        public async Task<IActionResult> GetRecommendProjectDetail(int projectId)
        {
            if (await _recommendService.IsRecommendProject(projectId, 1))
            {
                var project = await _projectQueries.GetProjectDetail(projectId);
                return Ok(project);
            }
            else
            {
                return BadRequest("无权限");
            }
        }
        /// <summary>
        /// CAP事件发送
        /// </summary>
        /// <returns></returns>
        [Route("cap")]
        [HttpGet]
        public IActionResult Cap()
        {
            var @event = new UserProfileChangedIntegrationEvent()
            {
                UserId = 1,
                Name = "Test",
                Company = "Test"
            };
            _capBus.Publish("UserProfileChanged", @event);
            return Ok();
        }
    }
}
