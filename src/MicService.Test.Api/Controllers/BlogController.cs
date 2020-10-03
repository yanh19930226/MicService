using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core;
using Core.Data.Domain.Bus;
using Core.Data.Domain.Models;
using Core.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicService.Test.Api.Application.Commands.Blogs;
using MicService.Test.Api.Application.Queries;
using MicService.Test.Api.Dto;
using MicService.Test.Api.Dto.Blogs;
using MicService.Test.Api.Models;

namespace MicService.Test.Api.Controllers
{
    /// <summary>
    /// Blog
    /// </summary>
    [Route("Api/Blog")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly IBlogQueries _q;
        private readonly IMediatorHandler _bus;
        public BlogController(IBlogQueries q, IMediatorHandler bus)
        {
            _bus = bus;
            _q = q;
        }
        /// <summary>
        /// 获取博客
        /// </summary>
        /// <returns></returns>
        [Route("GetBlogList")]
        [HttpGet]
        public CoreResult<IQueryable<Blog>> GetBlogList()
        {
            return _q.GetAll();
        }
        /// <summary>
        /// 分页获取博客
        /// </summary>
        /// <returns></returns>
        [Route("GetBlogPageList")]
        [HttpPost]
        public PageResult<IQueryable<Blog>> GetBlogPageList([FromBody]PostPageRequestDTO dto)
        {
            return _q.GetPage(dto);
        }
        /// <summary>
        /// 添加博客
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddBlog")]
        public async Task<CoreResult> Add([FromBody]BlogAddDto dto)
        {
            CoreResult result = new CoreResult();
            BlogAddCommand command = new BlogAddCommand(dto.Name, dto.Url);
            var res=await _bus.SendCommandAsync(command);
            if (res)
            {
                result.Success("添加成功");
            }
            else
            {
                result.Failed("添加失败");
            }
            return  result;
        }
        /// <summary>
        /// 修改博客
        /// </summary>
        /// <returns></returns>
        [Route("UpdateBlog")]
        [HttpPut]
        public async Task<CoreResult> Update([FromBody]BlogUpdateDto dto)
        {
            CoreResult result = new CoreResult();
            BlogUpdateCommand command = new BlogUpdateCommand(dto.Id,dto.Name, dto.Url);
            var res = await _bus.SendCommandAsync(command);
            if (res)
            {
                result.Success("修改成功");
            }
            else
            {
                result.Failed("修改失败");
            }
            return result;
        }
        /// <summary>
        /// 删除博客
        /// </summary>
        /// <returns></returns>
        [Route("DeleteBlog/{Id}")]
        [HttpDelete]
        public async Task<CoreResult> Delete(long Id)
        {

            CoreResult result = new CoreResult();
            BlogDeleteCommand command = new BlogDeleteCommand(Id);
            var res = await _bus.SendCommandAsync(command);
            if (res)
            {
                result.Success("删除成功");
            }
            else
            {
                result.Failed("删除成功");
            }
            return result;
        }
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [Route("Test")]
        [HttpGet]
        public IActionResult Test()
        {

            var entityTypes = Assembly.GetEntryAssembly().GetTypes()
           .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
           .Where(type => type.GetTypeInfo().IsClass)
           .Where(type => type.GetTypeInfo().BaseType != null)
           .Where(type => typeof(Entity).IsAssignableFrom(type)).ToList();
            return Ok(new {Time=DateTime.Now });
        }
        /// <summary>
        /// ValidateTest
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("ValidateTest")]
        [HttpPost]
        public IActionResult ValidateTest([FromBody]TestDto dto)
        {
            return Ok(new { Time = DateTime.Now });
        }
    }
}
