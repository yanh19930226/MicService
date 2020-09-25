using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Recommend.Api.Models
{
    public class ProjectRecommend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FromUserId { get; set; }
        public string FromUserName { get; set; }
        public string FromUserAvatar { get; set; }

        public int ProjectId { get; set; }
        public string ProjectAvatar { get; set; }
        public string Tags { get; set; }
        public string Company { get; set; }
        public string Introduction { get; set; }

        public DateTime? CreateTime { get; set; }
        public bool IsDel { get; set; }
    }
}
