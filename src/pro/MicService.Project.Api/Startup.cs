using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core;
using Core.Consul;
using Core.Logger;
using Core.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MicService.Project.Api.Infrastructure;

namespace MicService.Project.Api
{
    public class Startup : CommonStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void CommonServices(IServiceCollection services)
        {

            #region MediatR
            services.AddMediatR(typeof(Startup));
            #endregion

            #region MySql

            services.AddDbContext<ProjectContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString(""), b => b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });
            #endregion

            services.AddCoreSwagger()
                    .AddConsul()
                    .AddCoreSeriLog();
        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseConsul()
               .UseCoreSwagger();
        }
    }
}
