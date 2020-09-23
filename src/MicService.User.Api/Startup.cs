using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Swagger;
using Core.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Core.Consul;
using Core;
using MicService.User.Api.Models;
using Consul;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Options;
using Core.EventBus.Impletment.RabbitMq;
using MicService.User.Api.Integration.Event;
using MicService.User.Api.Integration.Handler;
using Autofac;
using AutoMapper;
using Core.Cap;

namespace MicService.User.Api
{
    public class Startup:CommonStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void CommonServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlUser"), sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            })
               .AddCoreSwagger()
               .AddConsul()
               .AddEventBus()
               .AddCap()
               .AddCoreSeriLog();

            services.AddScoped<UserProfileChangedIntegrationEventHandler>();
        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceLocator.ApplicationBuilder = app;
            app.UseCoreSwagger()
                  .UseEventBus(eventBus =>
                  {
                      eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
                  })
                  .UseConsul();
        }
    }
}
