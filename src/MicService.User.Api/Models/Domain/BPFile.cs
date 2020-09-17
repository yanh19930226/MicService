using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Models.Domain
{
    public class BPFile
    {
        /// <summary>
        /// BPid
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 原始地址
        /// </summary>
        public string OriginalPath { get; set; }
        /// <summary>
        /// 转化地址
        /// </summary>
        public string FormatPath { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTme { get; set; }

        //public virtual User User { get; set; }
    }
}
