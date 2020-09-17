using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MicService.Gateway.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region MyRegion
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc(Configuration["Swagger:Name"],
            //        new OpenApiInfo
            //        {
            //            Title = Configuration["Swagger:Title"],
            //            Version = Configuration["Swagger:Version"]
            //        });
            //}); 
            #endregion

            //var config = new ConfigurationBuilder().AddJsonFile("Ocelot.json").Build();
            services.AddControllers();
            services.AddOcelot();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region MyRegion
            //var apis = Configuration["Apis:SwaggerNames"].Split(";").ToList();
            //app.UseSwagger()
            //     .UseSwaggerUI(options =>
            //     {
            //         apis.ToList().ForEach(key =>
            //         {
            //             options.SwaggerEndpoint($"/{key}/swagger.json", key);
            //         });
            //         options.DocumentTitle = "apiÍø¹Ø";
            //     }); 
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot();
        }
    }
}
