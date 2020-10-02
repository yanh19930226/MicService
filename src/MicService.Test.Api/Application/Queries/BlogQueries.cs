using Core.Data.SeedWork;
using MicService.Test.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Application.Queries
{
    public class BlogQueries
    {
        public readonly IRepository<Blog> _blogRepository;
        public BlogQueries(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }
    }
}
