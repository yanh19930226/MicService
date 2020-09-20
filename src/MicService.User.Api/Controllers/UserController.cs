using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicService.User.Api.Models.Domain;

namespace MicService.User.Api.Controllers
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private UserContext _userContext;
        //private ILogger _logger;
        /// <summary>
        /// 用户服务
        /// </summary>
        /// <param name="userContext"></param>
        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }
        /// <summary>
        /// 全局异常测试
        /// </summary>
        /// <returns></returns>
        [Route("test")]
        [HttpGet]
        public IActionResult Test()
        {
            int divide = 1,divided=0;
            var res = divide / divided;
            return Json(res);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Route("getuser")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userContext.Users
                 .AsNoTracking()
                 .Include(u => u.Properties)
                 .SingleOrDefaultAsync(u => u.Id ==1);
            if (user == null)
                throw new CoreException($"错误的用户上下文id");
            return Json(user);

        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("getuserinfo/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            var user = await _userContext.Users
                 .AsNoTracking()
                 .Include(u => u.Properties)
                 .SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new CoreException($"错误的用户上下文id");
            return Json(new
            {
                UserId = user.Id,
                user.Name,
                user.Company,
                user.Avatar,
                user.Title
            });
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        [Route("patchuser")]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<Models.Domain.User> patch)
        {
            var user = await _userContext.Users
               .Include(u => u.Properties)
               .SingleOrDefaultAsync(u => u.Id == 1);
            foreach (var item in user?.Properties)
            {
                _userContext.UserProperties.Remove(item);
            }
            patch.ApplyTo(user);
            foreach (var item in user.Properties)
            {
                _userContext.UserProperties.Add(item);
            }
            _userContext.Update(user);
            _userContext.SaveChanges();
            return Json(user);
        }
        /// <summary>
        /// 检查或则创建用户
        /// </summary>
        /// <returns></returns>
        [Route("checkcreate")]
        [HttpPost]
        public async Task<IActionResult> CheckOrCreate([FromForm]string phone)
        {
            //ToDo:检查手机号码的格式
            var user = _userContext.Users.SingleOrDefault(q => q.Phone == phone);
            if (user == null)
            {
                user = new Models.Domain.User { Phone = phone };
                _userContext.Users.Add(user);
                await _userContext.SaveChangesAsync();
            }
            return Ok(new
            {
                UserId = user.Id,
                user.Name,
                user.Company,
                user.Avatar,
                user.Title
            });
        }

        /// <summary>
        /// 获取用户的标签
        /// </summary>
        /// <returns></returns>
        [Route("tags")]
        [HttpGet]
        public async Task<IActionResult> GetUserTags()
        {
            return Ok(await _userContext.UserTags.Where(u => u.UserId == 1).ToListAsync());
        }
        /// <summary>
        /// 根据手机号码查询用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("search")]
        [HttpPost]
        public async Task<IActionResult> Search(string phone)
        {
            return Ok(await _userContext.Users.Include(u => u.Properties).SingleOrDefaultAsync(q => q.Phone == phone));
        }
        /// <summary>
        /// 更新用户标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        [Route("tags")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserTags([FromBody] List<string> tags)
        {
            var originTags = _userContext.UserTags.Where(q => q.UserId == 1);
            var newTags = tags.Except(originTags.Select(t => t.Tag));
            await _userContext.UserTags.AddRangeAsync(newTags.Select(t => new UserTag
            {
                Tag = t,
                UserId = 1
            }));
            await _userContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// 服务发现
        /// </summary>
        /// <returns></returns>
        [Route("servicesdiscovery")]
        [HttpGet]
        public async Task<IActionResult>ServicesDiscovery()
        {
            var features = ServiceLocator.ApplicationBuilder.Properties["server.Features"] as FeatureCollection;
            var address = features.Get<IServerAddressesFeature>().Addresses.First();
            return Ok($"Info From ServiceA[{address}]");
        }
    }
}
