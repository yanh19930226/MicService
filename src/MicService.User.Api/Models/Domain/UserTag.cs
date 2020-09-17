using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Models.Domain
{
    public class UserTag
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; }

        public virtual User User { get; set; }
    }
}
