using Core.Data.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Application.Commands.Blogs
{
    public class BlogUpdateCommand:Command
    {
        public BlogUpdateCommand(long id,string name, string url)
        {
            id = Id;
            Name = name;
            Url = url;
        }
        public long Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
