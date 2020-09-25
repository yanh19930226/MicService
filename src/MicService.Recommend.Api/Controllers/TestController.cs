using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicService.Recommend.Api.Controllers
{
    [Route("api/recommend")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
