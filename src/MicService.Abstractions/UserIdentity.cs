using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Abstractions
{
    public class UserIdentity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; } = 1;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "yanh";
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; } = "Company";
        /// <summary>
        /// 工作岗位
        /// </summary>
        public string Title { get; set; } = "Title";
        // <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = "Avatar";
    }

    public class UserInfo
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
