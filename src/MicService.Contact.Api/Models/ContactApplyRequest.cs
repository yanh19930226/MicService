using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Contact.Api.Models
{
    public class ContactApplyRequest
    {
        // <summary>
        /// 申请用户id
        /// </summary>
        public int AppliedId { get; set; }
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
        // <summary>
        /// 同意或拒绝0未通过1通过
        /// </summary>
        public int Approvaled { get; set; }
        // <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandleTime { get; set; }
        // <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        // <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
    }
}
