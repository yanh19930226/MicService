using MicService.Abstractions;
using MicService.Abstractions.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicService.Contact.Api.Data
{
    public interface IContactRepository
    {
        /// <summary>
        /// 添加为好友
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AddContact(int userId, UserInfo user, CancellationToken cancellationToken);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateContactInfo(UserProfileChangedEvent user, CancellationToken cancellationToken);
        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<List<Models.Contact>> GetContactAsync(int userid, CancellationToken cancellationToken);
        /// <summary>
        /// 更新好友标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<bool> TagContactAsync(int userId, int contactId, List<string> tags, CancellationToken cancellationToken);
    }
}
