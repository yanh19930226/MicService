using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Recommend.Api.IntergrationEvent
{
    public class UserProfileChangedIntegrationEvent
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 工作岗位
        /// </summary>
        public string Title { get; set; }
        // <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
