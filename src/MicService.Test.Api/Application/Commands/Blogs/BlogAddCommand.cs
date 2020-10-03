using Core.Data.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Application.Commands.Blogs
{
    public class BlogAddCommand : Command
    {
        public  BlogAddCommand(string name, string url)
        {
            Name = name;
            Url = url;
        }
      
        public string Name { get; set; }
        
        public string Url { get; set; }
    }
}
