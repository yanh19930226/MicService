using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicService.Abstractions;
using MicService.Contact.Api.Data;
using MicService.Contact.Api.Services;

namespace MicService.Contact.Api.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : Controller
    {
        private IContactApplyRequestRepository _contactApplyRequestRepository;
        private IContactRepository _contactRepository;
        private IUserService _userService;
        public ContactController(IContactApplyRequestRepository contactApplyRequestRepository, IContactRepository contactRepository, IUserService userService)
        {
            _contactApplyRequestRepository = contactApplyRequestRepository;
            _contactRepository = contactRepository;
            _userService = userService;
        }
        /// <summary>
        /// 获取当前用户好友申请列表
        /// </summary>
        /// <returns></returns>
        [Route("applyrequestlist")]
        [HttpGet]
        public async Task<IActionResult> GetApplyRequestList(CancellationToken cancellationToken)
        {
            var result = await _contactApplyRequestRepository.GetRequestListAsync(1, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// 请求添加别人为好友(申请人自己)
        /// </summary>
        /// <param name="userId">被申请用户Id</param>
        /// <returns></returns>
        [Route("apply-request/{userId}")]
        [HttpPost]
        public async Task<IActionResult> AddApplyRequest(int userId, CancellationToken cancellationToken)
        {
            var baseUserInfo = await _userService.GetBaseUserInfoAsync(new UserIdentity().UserId);
            if (baseUserInfo == null)
            {
                throw new Exception("用户参数错误");
            }
            var result = await _contactApplyRequestRepository.AddRequestAsync(new Models.ContactApplyRequest
            {
                AppliedId = new UserIdentity().UserId,
                UserId = userId,
                Name = baseUserInfo.Name,
                Company = baseUserInfo.Company,
                Title = baseUserInfo.Title,
                Avatar = baseUserInfo.Avatar,
                CreateTime = DateTime.Now
            }, cancellationToken);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
