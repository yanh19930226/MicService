using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core;
using Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MicService.Test.Api.Data;

namespace MicService.Test.Api
{
    public class Startup : CommonStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void CommonServices(IServiceCollection services)
        {
            //services.AddDbContext<TestContext>(options =>
            //{
            //    options.UseMySql(Configuration.GetSection("ConnectionStrings:Zeus").Value, sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            //});
            services.AddCoreSwagger();

        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCoreSwagger();
        }
    }
}
