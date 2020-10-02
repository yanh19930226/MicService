using Core.Data.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Models
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
