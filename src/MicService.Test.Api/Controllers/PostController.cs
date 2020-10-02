using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicService.Test.Api.Controllers
{

    /// <summary>
    /// Post
    /// </summary>
    [Route("Api/Post")]
    [ApiController]
    public class PostController : Controller
    {
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [Route("Test")]
        [HttpGet]
        public IActionResult Test()
        {

            return Ok();
        }
    }
}