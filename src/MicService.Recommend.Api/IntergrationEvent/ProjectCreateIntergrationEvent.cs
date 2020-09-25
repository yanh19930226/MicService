using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Recommend.Api.IntergrationEvent
{
    public class ProjectCreateIntergrationEvent
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
