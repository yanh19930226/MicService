using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicService.Project.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    /// <returns></returns>
    [Route("api/project")]
    [ApiController]
    public class TestController : Controller
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
