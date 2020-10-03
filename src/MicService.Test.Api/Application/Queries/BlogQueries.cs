using Core.Data.Domain.Interfaces;
using Core.Extensions;
using Core.Result;
using Microsoft.EntityFrameworkCore;
using MicService.Test.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Application.Queries
{
    public class BlogQueries: IBlogQueries
    {
        public readonly IRepository<Blog> _blogRepository;
        public BlogQueries(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }
        /// <summary>
        /// 列表数据不分页
        /// </summary>
        /// <returns></returns>
        public CoreResult<IQueryable<Blog>> GetAll()
        {
            var result = new CoreResult<IQueryable<Blog>>();
            var post = _blogRepository.GetAll().AsNoTracking();
            if (post == null)
            {
                result.Failed("数据不存在");
                return result;
            }
            result.Success(post);
            return result;
        }
        /// <summary>
        /// 列表数据分页
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageResult<IQueryable<Blog>> GetPage(Dto.PostPageRequestDTO req)
        {
            var expression = LinqExtensions.True<Blog>();
            var postpage = _blogRepository.GetAll().ToPage(req.PageIndex, req.PageSize, expression, p => p.Id, true);
            return postpage;
        }
    }
}
