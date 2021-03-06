﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicService.User.Api.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
