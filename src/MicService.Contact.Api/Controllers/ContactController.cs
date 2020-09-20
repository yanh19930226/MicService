using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicService.Abstractions;
using MicService.Contact.Api.Data;
using MicService.Contact.Api.Models.ViewModel;
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
        [Route("applyrequest")]
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
        [Route("applyrequest/{userId}")]
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
                UserId = userId,
                AppliedId = new UserIdentity().UserId,
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
        /// <summary>
        /// 同意或者拒绝好友申请
        /// </summary>
        /// <returns></returns>
        [Route("applyrequest/{applierId}")]
        [HttpPut]
        public async Task<IActionResult> ApprovalApplyRequest(int applierId, CancellationToken cancellationToken)
        {
            var result = await _contactApplyRequestRepository.ApprovalAsync(new UserIdentity().UserId, applierId, cancellationToken);
            if (!result)
            {
                return BadRequest();
            }
            var applier = await _userService.GetBaseUserInfoAsync(applierId);
            var userinfo = await _userService.GetBaseUserInfoAsync(new UserIdentity().UserId);
            await _contactRepository.AddContact(new UserIdentity().UserId, applier, cancellationToken);
            await _contactRepository.AddContact(applierId, userinfo, cancellationToken);
            return Ok();
        }
        /// <summary>
        /// 获取用户联系人
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("getcontact")]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _contactRepository.GetContactAsync(new UserIdentity().UserId, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// 更新好友标签
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("tag")]
        [HttpPut]
        public async Task<IActionResult> TagContact([FromBody]TagInputViewModel model, CancellationToken cancellationToken)
        {
            var result = await _contactRepository.TagContactAsync(new UserIdentity().UserId, model.ContactId, model.Tags, cancellationToken);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
