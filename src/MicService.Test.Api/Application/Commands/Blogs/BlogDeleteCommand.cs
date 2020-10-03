using Core.Data.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Application.Commands.Blogs
{
    public class BlogDeleteCommand : Command
    {
        public BlogDeleteCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
