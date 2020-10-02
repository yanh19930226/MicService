using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicService.Test.Api.Application.Queries;
using MicService.Test.Api.Dto;

namespace MicService.Test.Api.Controllers
{
    /// <summary>
    /// Blog
    /// </summary>
    [Route("Api/Blog")]
    [ApiController]
    public class BlogController : Controller
    {
        //private readonly IBlogQueries _q;
        //public BlogController(IBlogQueries q)
        //{
        //    _q = q;
        //}
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [Route("Test")]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new {Time=DateTime.Now });
        }
        [Route("ValidateTest")]
        [HttpPost]
        public IActionResult ValidateTest([FromBody]TestDto dto)
        {
            return Ok(new { Time = DateTime.Now });
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <returns></returns>
        [Route("Name")]
        [HttpGet]
        public IActionResult Name()
        {
            Tester t = new Tester();
            var name=t.NameMethod();
            return Ok(new { Name = name });
        }
    }
}
