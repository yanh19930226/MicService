using Core.Result;
using MicService.Test.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Application.Queries
{
    public interface IBlogQueries
    {
        CoreResult<IQueryable<Blog>> GetAll();
        PageResult<IQueryable<Blog>> GetPage(Dto.PostPageRequestDTO req);
    }
}
