using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Consul;
using Core.Logger;
using Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicService.Project.Api
{
    public class Startup : CommonStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void CommonServices(IServiceCollection services)
        {
            services.AddCoreSwagger()
                 //.AddConsul()
               .AddCoreSeriLog();
        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app/*.UseConsul()*/
                 .UseCoreSwagger();
        }
    }
}
