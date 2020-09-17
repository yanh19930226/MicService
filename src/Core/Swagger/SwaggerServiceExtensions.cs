using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Swagger
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddCoreSwagger(this IServiceCollection services, IConfiguration configuration = null)
        {
            configuration = (configuration ?? services.BuildServiceProvider().GetService<IConfiguration>());
            SwaggerOption swaggerOption = configuration.GetSection("Swagger").Get<SwaggerOption>();
            bool enabled = swaggerOption.Enabled;
            if (!enabled)
            {
                return services; 
            }

            #region MiniProfiler
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
            }).AddEntityFramework();
            #endregion

            string title = swaggerOption.Title;
            int version = swaggerOption.Version;
            services.AddSwaggerGen(options =>
            {
                #region Authorization
                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };

                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion

                #region Info
                options.SwaggerDoc($"{title}", new OpenApiInfo() { Title = title, Version = $"{version}" });
                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    options.IncludeXmlComments(file, true);
                });
                #endregion
            });
            return services;
        }
        public static IApplicationBuilder UseCoreSwagger(this IApplicationBuilder app)
        {
            IConfiguration configuration = app.ApplicationServices.GetService<IConfiguration>();
            SwaggerOption swaggerOption = configuration.GetSection("Swagger").Get<SwaggerOption>();
            bool enabled = swaggerOption.Enabled;
            if (!enabled)
            {
                return app;
            }
            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "{documentName}/swagger.json";
                }).UseSwaggerUI(options =>
            {
                string title = swaggerOption.Title;
                int version = swaggerOption.Version;
                options.SwaggerEndpoint($"/{title}/swagger.json", $"{title} V{version}");

                options.DefaultModelsExpandDepth(-1); //设置为 - 1 可不显示models
                options.DocExpansion(DocExpansion.List);//设置为none可折叠所有方法
                #region EF生成的sql显示在swagger页面
                bool miniProfilerEnabled = swaggerOption.MiniProfiler;
                if (miniProfilerEnabled)
                {
                    options.IndexStream = () => new MiniPro().Min();
                }
                #endregion
            }).UseMiniProfiler();

            return app;
        }
    }
    /// <summary>
    /// MiniProfiler
    /// </summary>
    public class MiniPro
    {
        internal Stream Min()
        {
            var stream = GetType().Assembly.GetManifestResourceStream("Core.index.html");
            return stream;
        }
    }
}
