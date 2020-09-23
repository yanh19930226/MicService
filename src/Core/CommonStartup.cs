using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Consul;
using Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core
{
    public abstract class CommonStartup
    {
        public IConfiguration Configuration { get; }
        public CommonStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #region Core内置容器
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GloabalExceptionFilter)); 
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            this.CommonServices(services);

        }
        public abstract void CommonServices(IServiceCollection services);
        #endregion

        #region 第三方容器Autofac
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    this.SuppertContainer(container);
        //}

        //public abstract ContainerBuilder SuppertContainer(ContainerBuilder container);
        #endregion



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStateAutoMapper();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            this.CommonConfigure(app, env);
        }


        public abstract void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
