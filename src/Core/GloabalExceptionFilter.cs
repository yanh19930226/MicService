using Core.Result;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class GloabalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<GloabalExceptionFilter> _logger;
        public GloabalExceptionFilter(IHostingEnvironment env, ILogger<GloabalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            var json = new CoreResult();
            if (context.Exception.GetType() == typeof(CoreException))
            {
                json.Failed(context.Exception.Message);
                context.Result = new BadRequestObjectResult(json);
            }
            else
            {
                json.Failed("发生了未知错误");
                if (_env.IsDevelopment())
                {
                    json.Failed(context.Exception.StackTrace);
                }
                context.Result = new InternalServerErrorObjectResult(json);
            }
            _logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error) : base(error)
        {
            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
        }
    }
}
