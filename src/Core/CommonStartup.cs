using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Consul;
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

            var container = new ContainerBuilder();


            #region Mapper
            List<Profile> autoMapperProfiles = (Assembly.GetEntryAssembly()!.GetTypes()).Where(p => p.BaseType == typeof(Profile))
              .Select(p => (Profile)Activator.CreateInstance(p)).ToList();
            container.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (Profile item in autoMapperProfiles)
                {
                    cfg.AddProfile(item);
                }
            }));
            container.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope(); 
            #endregion

            this.CommonServices(services);
            //return new AutofacServiceProvider(container.Build());

        }
        public abstract void CommonServices(IServiceCollection services);
        #endregion


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
